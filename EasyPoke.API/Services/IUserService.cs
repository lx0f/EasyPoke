using EasyPoke.API.Models;

namespace EasyPoke.API.Services;

public interface IUserService
{
    User? GetUserByUsername(string username);
    User? GetUserByEmail(string email);
    bool RegisterUser(string username, string email, string password);
    bool ValidatePassword(string password);
    bool UpdateUserUsername(int id, string username);
}
