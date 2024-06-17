using System.Runtime.Serialization;

namespace Kwetter.Library.Messaging.Datatypes;

public enum Status
{
    [EnumMember] Created,
    [EnumMember] Failed,
    [EnumMember] Removed,
    [EnumMember] Success
}
