using AutoMapper;
using FluentAssertions;
using Isopoh.Cryptography.Argon2;
using Moq;
using UserService.Controllers;
using UserService.Core.Messaging.Handler;
using UserService.Core.Profiles;
using UserService.Core.Services;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;
using UserService.DAL.Model;
using UserService.DAL.Repository;

namespace UserServiceTest;
[TestFixture]
public class UnitServiceTests
{
    private Mock<IUserRepository> _userRepoMock;
    private IMapper mapper;
    private Mock<IUserMessageHandler> _messageHandlerMock;
    private UserServiceCore _userService;

    [SetUp]
    public void Setup()
    {
        _userRepoMock = new Mock<IUserRepository>();
        this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
        _messageHandlerMock = new Mock<IUserMessageHandler>();
        _userService = new UserServiceCore(_userRepoMock.Object, mapper, _messageHandlerMock.Object);
    }
    
        [Test]
        public async Task Create_ReturnsTrue_WhenUserCreationIsSuccessful()
        {
            //Arrange
            var model = new RegisterRequestBody { Username = "test", Password = "password" };

            _userRepoMock.Setup(s => s.Create(It.IsAny<UserModel>())).ReturnsAsync(true);

            //Act
            var result = await _userService.Create(model);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Create_ReturnsFalse_WhenUserCreationFails()
        {
            //Arrange
            var model = new RegisterRequestBody { Username = "test", Password = "password" };
            var userModel = new UserModel { Username = "test", Password = "hashedpassword" };

            _userRepoMock.Setup(s => s.Create(userModel)).ReturnsAsync(false);

            //Act
            var result = await _userService.Create(model);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task GetByLogin_ReturnsUser_WhenUserExists()
        {
            //Arrange
            string hashedpassword = Argon2.Hash("password");
            var requestModel = new LoginRequestBody() { Email = "test", Password = "password" };
            var returnedModel = new UserModel { Email = "test", Password = hashedpassword, UserID = Guid.NewGuid(), Username = "test"};
            var loginResponseBody = new LoginResponseBody() { UserID = returnedModel.UserID };
            _userRepoMock.Setup(s => s.GetUserByLogin(It.IsAny<UserModel>())).ReturnsAsync(returnedModel);
            
            //Act
            var result =  await _userService.GetByLogin(requestModel);
            
            //Assert
            result.Should().BeEquivalentTo(loginResponseBody);
        }

        [Test]
        public async Task GetByLogin_ReturnsNull_WhenUserDoesNotExist()
        {
            //Arrange
            var loginModel = new LoginRequestBody() { Email = "test", Password = "password" };
            var userModel = new UserModel { Email = "test", Password = "hashedpassword" };

            _userRepoMock.Setup(s => s.GetUserByLogin(userModel)).ReturnsAsync((UserModel)null);

            //Act
            var result = await _userService.GetByLogin(loginModel);

            //Assert
            result.Should().BeNull();
        }
    }
