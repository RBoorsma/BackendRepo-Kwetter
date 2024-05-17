using System.Runtime.Serialization;

namespace Kwetter.Library.Messaging.Enums;

public enum MessageQueue
{
    [EnumMember(Value = "Registration")] RegisterQueue,
}