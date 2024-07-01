using System.Collections.ObjectModel;
using KweetService.DAL.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace KweetService.DAL.Repository;

public class KweetRepository : IKweetRepository
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<KweetModel> collection;
    public KweetRepository(IConfiguration configuration)
    {
       
        client = new (Environment.GetEnvironmentVariable("MongoDB"));
        IMongoDatabase database = client.GetDatabase("Kweets");
        collection = database.GetCollection<KweetModel>("Kweets");
    }
   

    
    
    
    
    public Task AddKweetAsync(KweetModel kweet)
    {
       
        collection.InsertOneAsync(kweet);
        return Task.CompletedTask;
    }

    public Task UpdateKweetAsync(KweetModel kweet)
    {
        IMongoDatabase database = client.GetDatabase("Kweets");
        IMongoCollection<KweetModel> collection = database.GetCollection<KweetModel>("Kweets");
        collection.ReplaceOneAsync(x => x._id == kweet._id, kweet);
        throw new NotImplementedException();
    }

    public Task RemoveKweetAsync(KweetModel kweet)
    {
        IMongoDatabase database = client.GetDatabase("Kweets");
        IMongoCollection<KweetModel> collection = database.GetCollection<KweetModel>("Kweets");
        collection.DeleteOneAsync(x => x._id == kweet._id);
        throw new NotImplementedException();
    }
    public async Task<KweetModel?> GetKweetAsync(Guid guid)
    {
        return await collection.Find(x => x._id == guid).FirstOrDefaultAsync();
        
    }
    public async Task<List<KweetModel>?> GetKweetsByProfileAsync(Profile profile)
    {
        return await collection.Find(x => x.ProfileID == profile._id).ToListAsync();
       

    }
    
}
  