using Kwetter.Library.Messaging.Datatypes;
using Kwetter.Library.Messaging.Events;
using Messaging.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using UserService.Core.Messaging.Models;
using UserService.Core.Services;

namespace UserService.Core.Messaging.Handler;

public class UserMessageHandler : IUserMessageHandler
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly IRabbitMQReceiver<UserMessageBody> _receiver;

    private readonly PublishDataBuilder _messageBuilder = new();
    private readonly IRabbitMQPublisher _publisher;

    public UserMessageHandler(IServiceProvider provider, IRabbitMQPublisher publisher, IRabbitMQReceiver<UserMessageBody> receiver)
    {
        _serviceProvider = provider;
        this._receiver = receiver;
        this._publisher = publisher;
        
        _receiver.MessageReceived += OnMessageReceived;
      
    }


    

    public void SendStatus(UserMessageBody body)
    {
        MessageContainer<UserMessageBody> messageContainer = new MessageContainer<UserMessageBody>(body);
        IPublishData
            data = _messageBuilder.setBody(messageContainer).setRoutingKey(RoutingKey.Registration).build();
        _publisher.SendMessage(data);
    }

    public async void OnMessageReceived(object? sender, MessageReceivedEventArgs<UserMessageBody> eventArgs)
    {
        
        IServiceScopeFactory scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using IServiceScope scope = scopeFactory.CreateScope();
        {
            IServiceProvider scopedServices = scope.ServiceProvider;
            
            IUserService? userService = scopedServices.GetService<IUserService>();
            if (eventArgs.Data?.Status == Status.Created.ToString())
            {
                //Todo Implement Future Logic
            }
            else if (eventArgs.Data?.Status == Status.Failed.ToString())
            {
                await userService.RollbackOrDeleteCreation(eventArgs.Data.ID, eventArgs.Data.CorreletionID);
                
            }
        }
    }

    public void StartListening()
    {
        _receiver.StartListeningTo(MessageQueue.Registration);
    }
    
}