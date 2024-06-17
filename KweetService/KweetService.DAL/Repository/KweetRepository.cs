using System.Collections.ObjectModel;
using KweetService.DAL.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace KweetService.DAL.Repository;

public class KweetRepository(IConfiguration configuration) : IKweetRepository
{
    
    private MongoClient client = new MongoClient(configuration["MongoDB"]);

    public Task AddKweetAsync(KweetModel kweet)
    {
        IMongoDatabase database = client.GetDatabase("Kweets");
        IMongoCollection<KweetModel> collection = database.GetCollection<KweetModel>("Kweets");
        collection.InsertOneAsync(kweet);
        throw new NotImplementedException();
    }

    public Task UpdateKweetAsync(KweetModel kweet)
    {
        IMongoDatabase database = client.GetDatabase("Kweets");
        IMongoCollection<KweetModel> collection = database.GetCollection<KweetModel>("Kweets");
        collection.ReplaceOneAsync(x => x.KweetID == kweet.KweetID, kweet);
        throw new NotImplementedException();
    }

    public Task RemoveKweetAsync(KweetModel kweet)
    {
        IMongoDatabase database = client.GetDatabase("Kweets");
        IMongoCollection<KweetModel> collection = database.GetCollection<KweetModel>("Kweets");
        collection.DeleteOneAsync(x => x.KweetID == kweet.KweetID);
        throw new NotImplementedException();
    }
    public Task<KweetModel> GetKweetAsync(KweetModel kweet)
    {
        IMongoDatabase database = client.GetDatabase("Kweets");
        IMongoCollection<KweetModel> collection = database.GetCollection<KweetModel>("Kweets");
        collection.FindAsync(x => x.KweetID == kweet.KweetID);
        throw new NotImplementedException();
    }
    
    
    
}