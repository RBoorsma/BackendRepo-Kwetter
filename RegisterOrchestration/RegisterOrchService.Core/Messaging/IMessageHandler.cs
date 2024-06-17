namespace RegisterOrchService.Core.Messaging;

public interface IMessageHandler
{
    void StartListening();
    void SendStatus<T>(T body);
}