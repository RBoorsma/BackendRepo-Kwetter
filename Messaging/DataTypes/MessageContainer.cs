using System.Text.Json.Serialization;

namespace Kwetter.Library.Messaging.Datatypes;

public class MessageContainer<T> : IMessage
{
    public MessageContainer(T data)
    {
        this.Data = data;
    }
    
    [JsonPropertyName("data")]
    public T? Data { get; set; }
    
}
