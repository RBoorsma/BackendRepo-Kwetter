namespace KweetService.DAL.Model;

public class Profile
{
    public Profile(Guid id)
    {
        _id = id;
    }
    public Guid _id { get; set; }
}