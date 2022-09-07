using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public bool AddUser(User user)
    {
        _context.Users.Add(user);
        return true;
    }

    public User? GetUserByEmail(string email)
    {
        User? user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
        return user;
    }

    public User? GetUserByUsername(string username)
    {
        User? user = _context.Users.Where(u => u.Username == username).FirstOrDefault();
        return user;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
