using Microsoft.AspNetCore.Mvc;

namespace EasyPoke.API.Tests.Systems.Controllers;

public class TestUserController
{
    [Fact]
    public void RegisterUser_OnSuccess_ReturnCreated()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678";

        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.RegisterUser(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
                        .Returns(true);
        mockUserService
            .Setup(service => service.GetUserByUsername(It.IsAny<string>()))
            .Returns(new User()
            {
                Username = username,
                Email = email,
                Password = password
            });
        var controller = new UserController(mockUserService.Object);


        // Act
        var result = controller.RegisterUser(username, email, password);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = (CreatedAtActionResult)result;
        createdResult.Value.Should().BeOfType<User>();
    }

    [Fact]
    public void RegisterUser_OnFail_ReturnConflict()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678";

        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.RegisterUser(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
                        .Returns(false);

        var controller = new UserController(mockUserService.Object);

        // Act
        var result = controller.RegisterUser(username, email, password);

        // Assert
        result.Should().BeOfType<ConflictResult>();
    }
}
