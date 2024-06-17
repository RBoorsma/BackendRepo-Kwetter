using Kwetter.Library.Messaging.Datatypes;
using Kwetter.Library.Messaging.Events;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ.HandlerInterface;

public interface IPublishHandler
{
    public bool SendStatus<T>(T body);
  

}