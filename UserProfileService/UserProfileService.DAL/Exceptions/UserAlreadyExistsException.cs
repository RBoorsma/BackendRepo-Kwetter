namespace UserProfileService.DAL.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("User with the same email or username already exists.")
    {
    }

    public UserAlreadyExistsException(string message) : base(message)
    {
    }
}