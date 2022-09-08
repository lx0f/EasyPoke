using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IUserRepository
{
    void AddUser(User user);
    void DeleteUser(int id);
    User? GetUserByEmail(string email);
    User? GetUserById(int id);
    User? GetUserByUsername(string username);
    void UpdateUser(User user);
}

