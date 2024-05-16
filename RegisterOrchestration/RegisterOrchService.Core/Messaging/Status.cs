using System.Runtime.Serialization;

namespace RegisterOrchService.Core.Messaging;

public enum Status
{
[EnumMember(Value = "Created")] Created,
[EnumMember(Value = "Failed")] Failed,
[EnumMember(Value = "Removed")] Removed
}
