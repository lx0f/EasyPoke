using EasyPoke.API.Models;

namespace EasyPoke.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        User? user = _context.Users.Find(id);

        if (user is null)
            throw new KeyNotFoundException();

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public User? GetByEmail(string email)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }

    public User? Get(int id)
    {
        User? user = _context.Users.Find(id);
        return user;
    }

    public User? GetByUsername(string username)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Username == username);
        return user;
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
