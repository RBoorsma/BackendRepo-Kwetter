using Kwetter.Library.Messaging.Datatypes;
using Messaging.RabbitMQ;
using Kwetter.Library.Messaging.Events;
using Microsoft.Extensions.DependencyInjection;
using UserProfileService.Core.Messaging.Models;
using UserProfileService.Core.Service;
using UserProfileService.Core.ViewModel.ResponseBody;

namespace UserProfileService.Core.Messaging.Handler;

public class UserMessageHandler : IMessageHandler
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly IRabbitMQReceiver<UserMessageBody> _receiver;

    private readonly PublishDataBuilder _messageBuilder = new();
    private readonly IRabbitMQPublisher _publisher;
//TODO Look at the receiver, it needs a new <T> (Maybe we can remove it later but dont make it a priority)
    public UserMessageHandler(IServiceProvider provider, IRabbitMQPublisher publisher, IRabbitMQReceiver<UserMessageBody> receiver)
    {
        _serviceProvider = provider;
        this._receiver = receiver;
        this._publisher = publisher;
        
        _receiver.MessageReceived += OnMessageReceived;
      
    }
    

    public void SendStatus(UserMessageBody body)
    {
        IPublishData
            data = _messageBuilder.setBody(new MessageContainer<UserMessageBody>(body)).setRoutingKey(RoutingKey.Registration).build();
        _publisher.SendMessage(data);
    }

    private async void OnMessageReceived(object? sender, MessageReceivedEventArgs<UserMessageBody> eventArgs)
    {
        Console.WriteLine(eventArgs.Data.Status);
        IServiceScopeFactory scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using IServiceScope scope = scopeFactory.CreateScope();
        {
            IServiceProvider scopedServices = scope.ServiceProvider;

            IUserProfileService? userProfileService = scopedServices.GetService<IUserProfileService>();
            if (eventArgs.Data?.Status == Status.Created.ToString())
            {
                //Todo Implement Future Logic
            }
            else if (eventArgs.Data?.Status == Status.Failed.ToString())
            {
                await userProfileService.RollbackOrDeleteCreation(eventArgs.Data.UserID, eventArgs.Data.CorreletionID);
                
            }
        }
    }

    public void StartListening()
    {
        _receiver.StartListeningTo(MessageQueue.Registration);
    }
}