using EasyPoke.API.Models;

namespace EasyPoke.API.Services;

public interface IUserService
{
    User? GetUserByUsername(string username);
    bool RegisterUser(string username, string email, string password);
    bool ValidatePassword(string password);
}
