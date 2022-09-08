using EasyPoke.API.Models;

namespace EasyPoke.API.Services;

public interface IUserService
{
    bool DeleteUser(int id);
    User? GetUserByEmail(string email);
    User? GetUserById(int id);
    User? GetUserByUsername(string username);
    User? RegisterUser(UserRegisterInfo info);
    bool UpdateUserEmail(int id, string email);
    bool UpdateUserPassword(int id, string password);
    bool UpdateUserUsername(int id, string username);
}

