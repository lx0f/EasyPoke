using System.Text.RegularExpressions;
using EasyPoke.API.Models;
using EasyPoke.API.Repositories;

namespace EasyPoke.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public bool DeleteUser(int id)
    {
        User? user = _repository.Get(id);

        if (user is null)
            return false;

        _repository.Delete(id);
        return true;
    }

    public User? GetUserByEmail(string email)
    {
        User? user = _repository.GetByEmail(email);
        return user;
    }

    public User? GetUserById(int id)
    {
        User? user = _repository.Get(id);
        return user;
    }

    public User? GetUserByIdIncludePokemons(int id)
    {
        User? user = _repository.GetIncludePokemons(id);
        return user;
    }

    public User? GetUserByUsername(string username)
    {
        User? user = _repository.GetByUsername(username);
        return user;
    }

    public User? RegisterUser(UserRegisterInfo info)
    {
        User? userUsername = _repository.GetByUsername(info.Username);

        if (userUsername != null)
            return null;

        User? userEmail = _repository.GetByEmail(info.Email);

        if (userEmail != null)
            return null;

        bool isValid = ValidatePassword(info.Password);

        if (!isValid)
            return null;

        User user = new User()
        {
            Username = info.Username,
            Email = info.Email,
            Password = info.Password
        };

        _repository.Add(user);

        return user;
    }

    public bool UpdateUserEmail(int id, string email)
    {
        User? user = _repository.Get(id);

        if (user is null)
            return false;

        User? potentialUser = _repository.GetByEmail(email);

        if (potentialUser != null)
            return false;

        user.Email = email;
        _repository.Update(user);

        return true;
    }

    public bool UpdateUserPassword(int id, string password)
    {
        User? user = _repository.Get(id);

        if (user is null)
            return false;

        bool isValid = ValidatePassword(password);

        if (!isValid)
            return false;

        user.Password = password;
        _repository.Update(user);

        return true;
    }

    public bool UpdateUserUsername(int id, string username)
    {
        User? user = _repository.Get(id);

        if (user is null)
            return false;

        User? potentialUser = _repository.GetByUsername(username);

        if (potentialUser != null)
            return false;

        user.Username = username;
        _repository.Update(user);

        return true;
    }

    private bool ValidatePassword(string password)
    {
        Regex hasUppercase = new Regex(@"[A-Z]+");
        Regex hasLowercase = new Regex(@"[a-z]+");
        Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasMinimumEightLetters = new Regex(@".{8,}");

        bool isValid = hasUppercase.IsMatch(password)
                       && hasLowercase.IsMatch(password)
                       && hasNumber.IsMatch(password)
                       && hasMinimumEightLetters.IsMatch(password);

        return isValid;
    }
}
