using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EasyPoke.API.Tests.Systems.Controllers;

public class AuthTest
{
    private readonly WebApplicationFactory<Program> _factory;

    private const string _apiKey = "secret";

    public AuthTest()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Theory]
    [InlineData("/api/Auth")]
    public async Task GetAuthCheck_WithoutApiKey_ReturnUnauthorized(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.StatusCode.Should()
                           .Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("/api/Auth")]
    public async Task GetAuthCheck_WithApiKey_ReturnOk(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("ApiKey", _apiKey);

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.StatusCode.Should()
                           .Be(HttpStatusCode.OK);
    }
}
