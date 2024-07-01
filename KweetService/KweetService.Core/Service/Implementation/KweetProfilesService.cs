using KweetService.DAL.Model;
using KweetService.DAL.Repository;

namespace KweetService.Core.Service;

public class KweetProfilesService(IProfileRepository repository) : IKweetProfilesService
{
    public Task RollbackOrDeleteProfile(Profile profile)
    {
        throw new NotImplementedException();
    }

    public Task CreateProfileAsync(Profile profile)
    {
        repository.addProfile(profile);
        return Task.CompletedTask;
    }
}
