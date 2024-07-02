using UserProfileService.DAL.Model;

namespace UserProfileService.DAL.Repository;

public interface IUserProfileRepository
{
    Task<bool> Create(UserProfile userProfile);
    Task<bool> RollBackOrDeleteAsync(Guid guid);
    Task<UserProfile> GetProfile(UserProfile body);
}