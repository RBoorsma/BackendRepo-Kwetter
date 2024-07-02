

using Kwetter.Library.Messaging.Datatypes;
using Kwetter.Library.Messaging.Events;
using Messaging.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RegisterOrchService.Core.Services.Models;

namespace RegisterOrchService.Core.Messaging;

public class MessageHandler : IMessageHandler
{
    private readonly IServiceProvider _serviceProvider;

    private MQConnection _connection;
   // private readonly IRabbitMQReceiver<UserMessageBody> _receiver;

    private readonly PublishDataBuilder _messageBuilder = new();
    private readonly IRabbitMQPublisher _publisher;

    public MessageHandler(IServiceProvider provider, IRabbitMQPublisher publisher, IRabbitMQReceiver<UserMessageBody> receiver)
    {
        _serviceProvider = provider;
       // this._receiver = receiver;
        this._publisher = publisher;
        
       // _receiver.MessageReceived += OnMessageReceived;
      
    }
    
    

    public void SendStatus<T>(T body)
    {
        MessageContainer<T> messageContainer = new(body);
        IPublishData
            data = _messageBuilder.setBody(messageContainer).setRoutingKey(RoutingKey.Registration).build();
        _publisher.SendMessage(data);
    }

    public async void OnMessageReceived(object? sender, MessageReceivedEventArgs<UserMessageBody> eventArgs)
    {
        throw new NotImplementedException();


    }

    public void StartListening()
    {
        throw new NotImplementedException();
        //  _receiver.StartListeningTo(MessageQueue.Registration);
    }
}