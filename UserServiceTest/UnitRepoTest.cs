using FluentAssertions;
using Isopoh.Cryptography.Argon2;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UserService.DAL.Context;
using UserService.DAL.Exceptions;
using UserService.DAL.Model;
using UserService.DAL.Repository;

namespace UserServiceTest;

[TestFixture]
public class UserRepositoryTest
{
    private TestUserDbContext _context;
    private UserRepository _userRepository;

    [SetUp]
    public void Setup()
    {
        // Use in-memory database for testing
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<TestUserDbContext>()
            .UseSqlite(connection)
            .Options;
        
        _context = new TestUserDbContext(options);
        _context.Database.EnsureCreated();
        _userRepository = new UserRepository(_context);
        var users = new List<UserModel>();
        for (int i = 1; i <= 10; i++)
        {
            users.Add(new UserModel
            {
                Username = $"test{i}",
                Password = $"password{i}",
                Email = $"email{i}",
                UserID = Guid.NewGuid()
            });
        }

        _context.User.AddRange(users);
        _context.SaveChanges();
    }

    [Test]
    public async Task Create_ReturnsTrue_WhenUserCreationIsSuccessful()
    {
        // Arrange
        var userModel = new UserModel { Email = "test" , Username = "test", Password = "password" };

        // Act
        var result = await _userRepository.Create(userModel);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public async Task Create_ReturnsFalse_WhenVariableMailIsEmpty()
    {
        // Arrange
        var userModel = new UserModel {  Username = "test", Password = "password" };

        // Act
        var result = await _userRepository.Create(userModel);

        // Assert
        result.Should().BeFalse();
    }
    
    [Test]
    public async Task Create_ReturnsFalse_WhenVariableUsernameIsEmpty()
    {
        // Arrange
        var userModel = new UserModel {  Email= "hello", Password = "password" };

        // Act
        var result = await _userRepository.Create(userModel);

        // Assert
        result.Should().BeFalse();
    }
    [Test]
    public async Task Create_ShouldReturnFalse_WhenPasswordIsNull()
    {
        // Arrange
        var userModel = new UserModel { Email = "test@test.com", Username = "test" };

        // Act
        var result = await _userRepository.Create(userModel);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public async Task Create_ShouldReturnFalse_WhenUserAlreadyExists()
    {
        // Arrange
        var userModel = new UserModel { Email = "test1", Username = "test1", Password = "password1" };

        // Act
        Func<Task> result = async () => await _userRepository.Create(userModel);

        // Assert
        await result.Should().ThrowAsync<UserAlreadyExistsException>();
    }

    [Test]
    public async Task GetUserByLogin_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userModel = new UserModel { Email = "nonexistent@test.com" };

        // Act
        var result = await _userRepository.GetUserByLogin(userModel);

        // Assert
        result.Should().BeNull();
    }
    [Test]
    public async Task GetUserByLogin_ShouldReturnUser_WhenUserSuccesFullLogin()
    {
        // Arrange
        var userModel = new UserModel { Email = "email1", Password = "password1" };

        // Act
        var result = await _userRepository.GetUserByLogin(userModel);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserModel>();
        result.UserID.Should().NotBeEmpty();
    }


    [Test]
    public async Task RollBackOrDeleteUserAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        // Arrange
        var nonExistentGuid = Guid.NewGuid();

        // Act
        Func<Task> result = async () => await _userRepository.RollBackOrDeleteUserAsync(nonExistentGuid);

        // Assert
        await result.Should().ThrowAsync<NoRecordFoundException>();
    }
    
    [Test]
    public async Task RollBackOrDeleteUserAsync_ShouldReturnTrue_WhenUserIsDeleted()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        UserModel tempModel = new UserModel()
        {
            Email = "temp",
            Password = "temp",
            UserID = id,
            Username = "temp"
        };
        _context.User.Add(tempModel);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userRepository.RollBackOrDeleteUserAsync(id);

        // Assert
        result.Should().BeTrue();
    }
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
    
}