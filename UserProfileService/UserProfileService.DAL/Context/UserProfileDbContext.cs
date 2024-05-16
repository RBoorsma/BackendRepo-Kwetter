using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserProfileService.DAL.Model;

namespace UserProfileService.DAL.Context;
public class UserProfileDbContext : DbContext
{
    
    public DbSet<UserProfile> UserProfiles { get; set; }

    public UserProfileDbContext()
    {
        
    }

    public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.Write(Directory.GetCurrentDirectory());
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        string connectionString = configuration.GetConnectionString("AppDb")!;
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}