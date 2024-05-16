using UserProfileService.DAL.Model;

namespace UserProfileService.DAL.Repository;

public interface IUserProfileRepository
{
    Task Create(UserProfile userProfile);
    Task<bool> RollBackOrDeleteAsync(Guid guid);
}