using EasyPoke.API.Auth;
using EasyPoke.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoke.API.Controllers;

[ApiKeyAuth]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetUserByUsername(string username)
    {
        var user = _service.GetUserByUsername(username);
        
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public IActionResult RegisterUser(string username, string email, string password)
    {
        bool result = _service.RegisterUser(username, email, password);

        if (result)
        {
            var user = _service.GetUserByUsername(username);
            return CreatedAtAction(nameof(RegisterUser), user);
        }

        return Conflict();
    }
}
