using System.Collections.ObjectModel;
using KweetService.DAL.Model;
using MongoDB.Driver;

namespace KweetService.DAL.Repository;

public interface IKweetRepository
{
    Task AddKweetAsync(KweetModel kweet);
    

     Task UpdateKweetAsync(KweetModel kweet);
    

    Task RemoveKweetAsync(KweetModel kweet);
  

   Task<KweetModel?> GetKweetAsync(Guid guid);
   Task<List<KweetModel>?> GetKweetsByProfileAsync(Profile profile);





}
    