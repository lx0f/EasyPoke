using EasyPoke.API.Repositories;

namespace EasyPoke.API.Tests.Systems.Services;

public class TestUserService
{
    [Fact]
    public void RegisterUser_OnSuccess_ReturnTrue()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678Aa";

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.AddUser(It.IsAny<User>()))
            .Returns(true);
        var userService = new UserService(mockUserRepository.Object);

        // Act
        var result = userService.RegisterUser(username, email, password);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void RegisterUser_WithInvalidPassword_ReturnFalse()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678";

        var mockUserRepository = new Mock<IUserRepository>();
        var userService = new UserService(mockUserRepository.Object);

        // Act
        var result = userService.RegisterUser(username, email, password);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void RegisterUser_WithExistingUsername_ReturnFalse()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678Aa";

        List<User> mockUserDbSet = new()
        {
            new()
            {
                Id = 1,
                Username = username,
                Email = email,
                Password = password
            }
        };

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.GetUserByUsername(It.IsAny<string>()))
            .Returns((string username) => mockUserDbSet.Single(u => u.Username == username));
        var userService = new UserService(mockUserRepository.Object);

        // Act
        var result = userService.RegisterUser(username, email, password);

        // Arrange
        result.Should().BeFalse();
    }

    [Fact]
    public void RegisterUser_WithExistingEmail_ReturnFalse()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678Aa";

        List<User> mockUserDbSet = new()
        {
            new()
            {
                Id = 1,
                Username = "newuser",
                Email = email,
                Password = password
            }
        };

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
            .Returns((string email) => mockUserDbSet.Single(u => u.Email == email));
        var userService = new UserService(mockUserRepository.Object);

        // Act
        var result = userService.RegisterUser(username, email, password);

        // Arrange
        result.Should().BeFalse();
    }
}
