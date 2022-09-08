using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        User? user = _context.Users.Find(id);

        if (user == null)
            throw new KeyNotFoundException();

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }

    public User? GetUserById(int id)
    {
        User? user = _context.Users.Find(id);
        return user;
    }

    public User? GetUserByUsername(string username)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Username == username);
        return user;
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
