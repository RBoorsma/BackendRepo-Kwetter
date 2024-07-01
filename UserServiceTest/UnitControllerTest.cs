using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserService.Controllers;
using UserService.Core.Services;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;

namespace UserServiceTest;
[TestFixture]
public class UserControllerTests
{
    private Mock<IUserService> _userServiceMock;
    private UserController _userController;


    [SetUp]
    public void Setup()
    {
        _userServiceMock = new Mock<IUserService>();
        _userController = new UserController(_userServiceMock.Object);
    }

    [Test]
    public async Task Create_ReturnsCreated_WhenUserCreationIsSuccessful()
    {
        //Arrange
        var model = new RegisterRequestBody { Username = "test", Password = "password" };

        _userServiceMock.Setup(s => s.Create(model)).ReturnsAsync(true);

        //Act
        var result = await _userController.Create(model);
        //Assert
        result.Should().BeOfType<CreatedResult>();
    }

    [Test]
    public async Task Create_ReturnsStatusCode500_WhenUserCreationFails()
    {
        //Arrange
        var model = new RegisterRequestBody { Username = "test", Password = "password" };

        _userServiceMock.Setup(s => s.Create(model)).ReturnsAsync(false);
        //Act   
        var result = await _userController.Create(model);
        //Assert
        result.Should().BeOfType<StatusCodeResult>();
        ((StatusCodeResult)result).StatusCode.Should().Be(500);
    }

    [Test]
    public async Task GetByLogin_ReturnsOk_WhenUserExists()
    {
        //Arrange
        var loginModel = new LoginRequestBody() { Email = "test", Password = "password" };

        LoginResponseBody answer = new LoginResponseBody()
        {
            UserID = Guid.NewGuid()
        };
        _userServiceMock.Setup(s => s.GetByLogin(loginModel)).ReturnsAsync(answer);
        //Act
        var result = await _userController.GetByLogin(loginModel);
        var okresult = result as OkObjectResult;
        var responsebody = okresult.Value as LoginResponseBody;
        //Assert
        result.Should().BeOfType<OkObjectResult>();
        responsebody.UserID.Should().NotBeEmpty();
    }
    
    [Test]
    public async Task GetByLogin_ReturnsNotFound_WhenUserDoesNotExist()
    {
        //Arrange
        var loginModel = new LoginRequestBody() { Email = "test", Password = "password" };
        LoginResponseBody answer = null;
        _userServiceMock.Setup(s => s.GetByLogin(loginModel)).ReturnsAsync(answer);
        //Act
        var result = await _userController.GetByLogin(loginModel);
        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}