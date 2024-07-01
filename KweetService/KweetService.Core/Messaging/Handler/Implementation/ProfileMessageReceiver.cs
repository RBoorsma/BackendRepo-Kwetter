using System.IO.Compression;
using KweetService.Core.Service;
using KweetService.DAL.Model;
using Kwetter.Library.Messaging.Datatypes;
using Kwetter.Library.Messaging.Events;
using Messaging.RabbitMQ;
using Messaging.RabbitMQ.HandlerInterface;

namespace KweetService.Core.Messaging.Handler;

public class ProfileMessageReceiver : IReceiverHandler<DefaultMessageData>
{
    
    private readonly IRabbitMQReceiver<DefaultMessageData> receiver;
    private readonly IKweetProfilesService service;

    public ProfileMessageReceiver(IRabbitMQReceiver<DefaultMessageData> receiver, IKweetProfilesService service)
    {
        this.receiver = receiver;
        this.service = service;
        this.receiver.MessageReceived += OnMessageReceived;

    }
    
    public void OnMessageReceived(object? sender, MessageReceivedEventArgs<DefaultMessageData> eventArgs)
    {
        DefaultMessageData data = eventArgs.Data;
        Profile profile = new(data.ID);
        if (eventArgs.Routingkey == "profile.created")
        {
            
            service.CreateProfileAsync(profile);
        }
       
        else if (eventArgs.Routingkey == "profile.deleted")
        {
            service.RollbackOrDeleteProfile(profile);
        }
    }

    public void StartListening()
    {
        receiver.StartListeningToCustom("profile.created");
        
    }
}