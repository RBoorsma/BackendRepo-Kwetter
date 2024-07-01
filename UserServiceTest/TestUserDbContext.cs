using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UserService.DAL.Context;

public class TestUserDbContext : UserDbContext
{
    public TestUserDbContext(DbContextOptions<TestUserDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        optionsBuilder.UseSqlite(connection);
    }
}