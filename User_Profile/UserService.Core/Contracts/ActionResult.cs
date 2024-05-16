using System.Runtime.Serialization;

namespace UserService.Core.Contracts;

public enum ActionResult
{
    [EnumMember(Value = "Ok")] Ok = 200,

    [EnumMember(Value = "Not Found")] NotFound = 404,

    [EnumMember(Value = "Error")] Error = 500
}