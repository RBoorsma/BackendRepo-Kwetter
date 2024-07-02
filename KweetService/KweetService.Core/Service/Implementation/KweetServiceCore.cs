using System.Collections.ObjectModel;
using KweetService.Core.Messaging.ViewModels;
using KweetService.DAL.Model;
using KweetService.DAL.Repository;

namespace KweetService.Core.Service;

public class KweetServiceCore(IKweetRepository repository) : IKweetService
{
    public Task<bool> AddKweetAsync(KweetModel kweet)
    {
        repository.AddKweetAsync(kweet);
        return Task.FromResult(true);
    }

    public Task<List<KweetModel>?> GetKweetsByProfileAsync(ProfileRequestBody body)
    {
        Profile profile = new Profile(body._id);
        return repository.GetKweetsByProfileAsync(profile);
    }
    public Task<KweetModel?> GetKweetByID(Guid guid)
    {
        return repository.GetKweetAsync(guid);
    }
}