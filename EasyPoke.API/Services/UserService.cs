using EasyPoke.API.Models;
using EasyPoke.API.Repositories;
using System.Text.RegularExpressions;

namespace EasyPoke.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User? GetUserByEmail(string email)
    {
        return _repository.GetUserByEmail(email);
    }

    public User? GetUserByUsername(string username)
    {
        return _repository.GetUserByUsername(username);
    }

    public bool RegisterUser(string username, string email, string password)
    {
        bool isValid = ValidatePassword(password);

        if (!isValid)
        {
            return false;
        }

        User? possibleUser1 = _repository.GetUserByUsername(username);

        if (possibleUser1 != null)
        {
            return false;
        }

        User? possibleUser2 = _repository.GetUserByEmail(email);

        if (possibleUser2 != null)
        {
            return false;
        }

        User user = new()
        {
            Username = username,
            Email = email,
            Password = password
        };

        bool result = _repository.AddUser(user);
        _repository.Save();
        return true;
    }

    public bool ValidatePassword(string password)
    {
        Regex hasUppercaseChar = new Regex(@"[A-Z]+");
        Regex hasLowercaseChar = new Regex(@"[a-z]+");
        Regex hasOneNumber = new Regex(@"[0-9]+");
        Regex hasMinimumEightChars = new Regex(@".{8,}");

        bool isValid =
            hasUppercaseChar.IsMatch(password)
            && hasLowercaseChar.IsMatch(password)
            && hasOneNumber.IsMatch(password)
            && hasMinimumEightChars.IsMatch(password);

        return isValid;
    }
}
