using System.Text.Json;
using Kwetter.Library.Messaging.Enums;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using UserProfileService.Core.Messaging.Events;
using UserProfileService.Core.Messaging.Models;
using UserProfileService.Core.Messaging.RabbitMQ;
using UserProfileService.Core.Service;
using UserProfileService.DAL.Repository;



namespace UserProfileService.Core.Messaging.Handler;

public class MessageHandler : IMessageHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRabbitMqConsumer _mqConsumer;
    private readonly IRabbitMqPublisher _mqPublisher;

    public MessageHandler(IRabbitMqConsumer mqConsumer, IRabbitMqPublisher mqPublisher,
        IServiceProvider serviceProvider)
    {
        _mqConsumer = mqConsumer;
        _serviceProvider = serviceProvider;
        _mqPublisher = mqPublisher;
        mqConsumer.MessageReceived += OnMessageReceived;
    }

    public void StartListening()
    {
        _mqConsumer.Receive();
    }

    public void SendStatus(UserRequestBody body)
    {
        string jsonData = JsonSerializer.Serialize(body);
        _mqPublisher.SendProfileStatus(body.CorreletionID, jsonData);
    }

    public async void OnMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        IServiceScopeFactory scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using IServiceScope scope = scopeFactory.CreateScope();
        {
            IServiceProvider scopedServices = scope.ServiceProvider;

            IUserProfileService? userProfileService = scopedServices.GetService<IUserProfileService>()!;
            if (e.Data.Status == Status.Created.ToString())
            {
                //To Implement Future Logic
            }
            else if (e.Data.Status == Status.Failed.ToString())
            {
                await userProfileService.RollbackOrDeleteCreation(e.Data.UserID, e.Data.CorreletionID);
            }
        }
    }
}