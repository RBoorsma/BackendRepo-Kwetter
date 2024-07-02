using System.Collections.ObjectModel;
using KweetService.Core.Messaging.ViewModels;
using KweetService.DAL.Model;

namespace KweetService.Core.Service;

public interface IKweetService
{
    Task<bool> AddKweetAsync(KweetModel kweet);
    Task<List<KweetModel>?> GetKweetsByProfileAsync(ProfileRequestBody profile);
    Task<KweetModel?> GetKweetByID(Guid guid);
}