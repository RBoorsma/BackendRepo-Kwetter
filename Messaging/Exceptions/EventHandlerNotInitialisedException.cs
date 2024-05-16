namespace Messaging.Exceptions;

public class EventHandlerNotInitialisedException : Exception
{
    public EventHandlerNotInitialisedException() : base("The EventHandler hasn't been Initialised! Try to run SetupEventHandler() first!")
    {
    }

    public EventHandlerNotInitialisedException(string message) : base(message)
    {
    }
}
