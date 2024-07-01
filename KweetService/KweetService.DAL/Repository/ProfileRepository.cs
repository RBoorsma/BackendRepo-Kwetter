using KweetService.DAL.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace KweetService.DAL.Repository;

public class ProfileRepository : IProfileRepository
{
    
    
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<Profile> collection;
    public ProfileRepository(IConfiguration configuration)
    {
       
        client = new (Environment.GetEnvironmentVariable("MongoDB"));
        IMongoDatabase database = client.GetDatabase("Kweets");
        collection = database.GetCollection<Profile>("Users");
    }

    public Task addProfile(Profile profile)
    {
        collection.InsertOneAsync(profile);
        return Task.CompletedTask;
    }
}