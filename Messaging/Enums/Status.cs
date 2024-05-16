using System.Runtime.Serialization;

namespace Kwetter.Library.Messaging.Enums;

public enum Status
{
    [EnumMember(Value = "Created")] Created,
    [EnumMember(Value = "Failed")] Failed,
    [EnumMember(Value = "Removed")] Removed,
    [EnumMember(Value = "Success")] Success
}
