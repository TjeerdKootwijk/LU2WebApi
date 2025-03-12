using LU2WebApi.Controllers;
using LU2WebApi.Models;
using LU2WebApi.Repositories.Environment;
using LU2WebApi.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;


namespace UnitTestProject
{
    [TestClass]
    public sealed class EnvironmentTest
    {
        private Mock<IAuthenticationService> _mockAuthService;
        private Mock<IEnvironment2DRepository> _mockRepo;
        private Mock<IGetUserRepository> _mockUserRepo;
        private Mock<ILogger<Environment2DController>> _mockLogger;
        private Environment2DController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockRepo = new Mock<IEnvironment2DRepository>();
            _mockUserRepo = new Mock<IGetUserRepository>();
            _mockLogger = new Mock<ILogger<Environment2DController>>();

            _controller = new Environment2DController(_mockAuthService.Object, _mockRepo.Object, _mockLogger.Object, _mockUserRepo.Object);
        }

        [TestMethod]
        public async Task CreateEnvironment2D_UserNotLoggedIn_ReturnsBadRequest()
        {
            _mockAuthService.Setup(a => a.GetCurrentAuthenticatedUserId()).Returns((string)null);
            var result = await _controller.Create(new Environment2DDTO { Name = "TestWorld", MaxWidth = 50, MaxHeight = 50 });
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task CreateEnvironment2D_InvalidName_ReturnsBadRequest()
        {
            _mockAuthService.Setup(a => a.GetCurrentAuthenticatedUserId()).Returns(Guid.NewGuid().ToString());
            var result = await _controller.Create(new Environment2DDTO { Name = "", MaxWidth = 50, MaxHeight = 50 });
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task CreateEnvironment2D_MaxWorldsReached_ReturnsBadRequest()
        {
            var userId = Guid.NewGuid().ToString();
            _mockAuthService.Setup(a => a.GetCurrentAuthenticatedUserId()).Returns(userId);
            var existingEnvironments = GenerateUserEnvironments(userId, 5);
            _mockRepo.Setup(x => x.GetEnvironment2DOfUser(Guid.Parse(userId))).ReturnsAsync(existingEnvironments);

            var result = await _controller.Create(new Environment2DDTO { Name = "NewWorld", MaxWidth = 50, MaxHeight = 50 });
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task CreateEnvironment2D_ValidRequest_ReturnsCreated()
        {
            var userId = Guid.NewGuid().ToString();
            _mockAuthService.Setup(a => a.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockRepo.Setup(x => x.GetEnvironment2DOfUser(Guid.Parse(userId))).ReturnsAsync(new List<Environment2D>());
            _mockRepo.Setup(x => x.CreateEnvironment2D(It.IsAny<Environment2DDTO>(), userId)).ReturnsAsync(Guid.NewGuid());

            var result = await _controller.Create(new Environment2DDTO { Name = "ValidWorld", MaxWidth = 50, MaxHeight = 50 });
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }
        private List<Environment2D> GenerateUserEnvironments(string userId, int count, string name = "World")
        {
            var list = new List<Environment2D>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new Environment2D
                {
                    Id = Guid.NewGuid(),
                    Name = name + i,
                    MaxWidth = 100,
                    MaxHeight = 100
                });
            }
            return list;
        }
    }
}
