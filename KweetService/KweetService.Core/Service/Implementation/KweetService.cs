using System.Collections.ObjectModel;
using KweetService.DAL.Model;

namespace KweetService.Core.Service;

public class KweetService : IKweetService
{
    public Task<bool> AddKweetAsync(KweetModel kweet)
    {
        throw new NotImplementedException();
    }

    public Task<Collection<KweetModel?>> GetKweetsByProfileAsync(Profile profile)
    {
        throw new NotImplementedException();
    }
}