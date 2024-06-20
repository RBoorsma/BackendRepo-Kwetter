namespace UserServiceTest;

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
            var model = new RegisterRequestBody { Username = "test", Password = "password" };

            _userServiceMock.Setup(s => s.Create(model)).ReturnsAsync(true);

            var result = await _userController.Create(model);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task Create_ReturnsStatusCode500_WhenUserCreationFails()
        {
            var model = new RegisterRequestBody { Username = "test", Password = "password" };

            _userServiceMock.Setup(s => s.Create(model)).ReturnsAsync(false);

            var result = await _userController.Create(model);

            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(500, ((StatusCodeResult)result).StatusCode);
        }

        [Test]
        public async Task GetByLogin_ReturnsOk_WhenUserExists()
        {
            var loginModel = new LogReinquestBody { Username = "test", Password = "password" };

            _userServiceMock.Setup(s => s.GetByLogin(loginModel)).ReturnsAsync(new UserService.Core.ViewModel.ResponseBody.LoginResponseBody());

            var result = await _userController.GetByLogin(loginModel);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetByLogin_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var loginModel = new LoginRequestBody { Username = "test", Password = "password" };

            _userServiceMock.Setup(s => s.GetByLogin(loginModel)).ReturnsAsync((UserService.Core.ViewModel.ResponseBody.LoginResponseBody)null);

            var result = await _userController.GetByLogin(loginModel);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}