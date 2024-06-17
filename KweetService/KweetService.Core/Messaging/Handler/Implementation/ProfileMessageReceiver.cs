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
    private readonly IKweetProfilesService repository;

    public ProfileMessageReceiver(IRabbitMQReceiver<DefaultMessageData> receiver)
    {
        this.receiver = receiver;
        this.repository = repository;
        this.receiver.MessageReceived += OnMessageReceived;

    }
    
    public void OnMessageReceived(object? sender, MessageReceivedEventArgs<DefaultMessageData> eventArgs)
    {
        DefaultMessageData data = eventArgs.Data;
        Profile profile = new(data.Id);
        if (data.Type == "profile.created")
        {
            
            repository.CreateProfileAsync(profile);
        }
       
        else if (data.Type == "profile.deleted")
        {
            repository.RollbackOrDeleteProfile(profile);
        }



        throw new NotImplementedException();
    }

    public void StartListening()
    {
        receiver.StartListeningToCustom("profile.created");
        
    }
}