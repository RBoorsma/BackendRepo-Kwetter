namespace KweetService.DAL.Model;

public class Profile
{
    public Profile(Guid profileId)
    {
        ProfileID = profileId;
    }
    public Guid ProfileID { get; set; }
}