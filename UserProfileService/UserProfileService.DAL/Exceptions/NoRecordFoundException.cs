namespace UserProfileService.DAL.Exceptions;

public class NoRecordFoundException : Exception
{
    public NoRecordFoundException() : base("The given entry could not be found")
    {
    }

    public NoRecordFoundException(string message) : base(message)
    {
    }
}