using System.Runtime.Serialization;

namespace Kwetter.Library.Messaging.Enums;

public enum RoutingKey
{
    [EnumMember(Value = "Registration")] RegisterQueue
}
   