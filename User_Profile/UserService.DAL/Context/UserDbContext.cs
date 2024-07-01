using Microsoft.EntityFrameworkCore;
using UserService.DAL.Model;
using Microsoft.Extensions.Configuration;
namespace UserService.DAL.Context;

public class UserDbContext : DbContext
{
    public DbSet<UserModel> User { get; set; }

    public UserDbContext()
    {
        
    }

    public UserDbContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Console.Write(Directory.GetCurrentDirectory());
        // IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("appsettings.json").Build();

       // string connectionString = configuration.GetConnectionString("AppDb")!;
       string environmentVariable = Environment.GetEnvironmentVariable("MySQL");
       string connectionString = environmentVariable + ";database=Users;";
       
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
        
}