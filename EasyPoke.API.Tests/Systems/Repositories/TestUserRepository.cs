using EasyPoke.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyPoke.API.Tests.Systems.Repositories;

public class TestUserRepository
{
    private readonly DataContext context;

    public TestUserRepository()
    {
        DbContextOptionsBuilder options = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString());
        context = new DataContext(options.Options);
    }

    [Fact]
    public void AddUser_OnSuccess_ReturnTrue()
    {
        // Arrange
        User user = new()
        {
            Username = "testuser",
            Email = "testuser@example.com",
            Password = "12345678Aa"
        };

        UserRepository repository = new(context);

        // Act
        var result = repository.AddUser(user);
        repository.Save();

        // Arrange
        List<User> users = context.Users.ToList();
        result.Should().BeTrue();
        users.Should().HaveCount(1);
    }

    [Fact]
    public void GetUserByUsername_OnSuccess_ReturnUser()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678Aa";

        context.Users.Add(new User()
        {
            Id = 1,
            Username = username,
            Email = email,
            Password = password
        });
        context.SaveChanges();

        UserRepository repository = new(context);

        // Act
        User? user = repository.GetUserByUsername(username);

        // Arrange
        user.Should().BeOfType<User>();
        if (user != null)
            user.Username.Should().Be(username);
    }

    [Fact]
    public void GetUserByEmail_OnSuccess_ReturnUser()
    {
        // Arrange
        string username = "testuser";
        string email = "testuser@example.com";
        string password = "12345678Aa";

        context.Users.Add(new User()
        {
            Id = 1,
            Username = username,
            Email = email,
            Password = password
        });
        context.SaveChanges();

        UserRepository repository = new(context);

        // Act
        User? user = repository.GetUserByEmail(email);

        // Arrange
        user.Should().BeOfType<User>();
        if (user != null)
            user.Email.Should().Be(email);
    }
}
