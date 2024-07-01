using Kwetter.Library.Messaging.Datatypes;
using Messaging.RabbitMQ;
using Kwetter.Library.Messaging.Events;
using Microsoft.Extensions.DependencyInjection;
using UserProfileService.Core.Messaging.Models;
using UserProfileService.Core.Service;
using UserProfileService.Core.ViewModel.ResponseBody;

namespace UserProfileService.Core.Messaging.Handler;

public class ProfileMessageHandler : IMessageHandler
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly IRabbitMQReceiver<MessagingBody> _receiver;

    private readonly PublishDataBuilder _messageBuilder = new();
    private readonly IRabbitMQPublisher _publisher;
//TODO Look at the receiver, it needs a new <T> (Maybe we can remove it later but dont make it a priority)
    public ProfileMessageHandler(IServiceProvider provider, IRabbitMQPublisher publisher, IRabbitMQReceiver<MessagingBody> receiver)
    {
        _serviceProvider = provider;
        this._receiver = receiver;
        this._publisher = publisher;
        DeclareQueue();
        
        _receiver.MessageReceived += OnMessageReceived;
      
    }
    public void SendStatusCustom(MessagingBody body, string key)
    {
        if(body.CorreletionID == Guid.Empty)
            body.CorreletionID = Guid.NewGuid();
        IPublishData
            data = _messageBuilder.setBody(new MessageContainer<MessagingBody>(body)).setCustomRoutingKey(key).build();
        _publisher.SendMessage(data);
    }

    public void SendStatus(MessagingBody body, RoutingKey routingKey = RoutingKey.Registration)
    {
        IPublishData
            data = _messageBuilder.setBody(new MessageContainer<MessagingBody>(body)).setRoutingKey(routingKey).build();
        _publisher.SendMessage(data);
    }

    private async void OnMessageReceived(object? sender, MessageReceivedEventArgs<MessagingBody> eventArgs)
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
                await userProfileService.RollbackOrDeleteCreation(eventArgs.Data.ID, eventArgs.Data.CorreletionID);
                
            }
        }
    }

    private void DeclareQueue()
    {
        List<string> queues = new()
        {
            "profile.created"
        };
        foreach (string queue in queues)
        {
            _publisher.DeclareQueues(queue);
        }
    }
    public void StartListening()
    {
      //  _receiver.StartListeningTo(MessageQueue.Registration);
    }
}