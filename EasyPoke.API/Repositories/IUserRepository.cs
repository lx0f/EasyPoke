using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public interface IUserRepository
{
    void Add(User user);
    void Delete(int id);
    User? GetByEmail(string email);
    User? Get(int id);
    User? GetByUsername(string username);
    void Update(User user);
}

