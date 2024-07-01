using System.Collections.ObjectModel;
using KweetService.DAL.Model;

namespace KweetService.Core.Service;

public interface IKweetService
{
    Task<bool> AddKweetAsync(KweetModel kweet);
    Task<List<KweetModel>?> GetKweetsByProfileAsync(Profile profile);
    Task<KweetModel?> GetKweetByID(Guid guid);
}