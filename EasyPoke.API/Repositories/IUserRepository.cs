using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IUserRepository
{
    bool AddUser(User user);
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
    void Save();
}
