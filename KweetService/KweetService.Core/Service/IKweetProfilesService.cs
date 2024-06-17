using KweetService.DAL.Model;

namespace KweetService.Core.Service;

public interface IKweetProfilesService
{
    public Task RollbackOrDeleteProfile(Profile profile);


    public Task CreateProfileAsync(Profile profile);

}