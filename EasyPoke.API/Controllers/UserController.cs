using EasyPoke.API.Auth;
using EasyPoke.API.Models;
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

    [HttpGet("{id}", Name = "GetUserById")]
    public IActionResult GetUserById(int id)
    {
        User? user = _service.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("username", Name = "GetUserByUsername")]
    public IActionResult GetUserByUsername(string username)
    {
        User? user = _service.GetUserByUsername(username);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("email", Name = "GetUserByEmail")]
    public IActionResult GetUserByEmail(string email)
    {
        User? user = _service.GetUserByEmail(email);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("register", Name = "RegisterUser")]
    public IActionResult RegisterUser(UserRegisterInfo info)
    {
        User? user = _service.RegisterUser(info);

        if (user == null)
            return BadRequest();

        return CreatedAtAction(nameof(RegisterUser), user);
    }

    [HttpPatch("{id}/username", Name = "UpdateUsername")]
    public IActionResult UpdateUserUsername(int id, string username)
    {
        User? user = _service.GetUserById(id);

        if (user == null)
            return NotFound();

        bool result = _service.UpdateUserUsername(id, username);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/email", Name = "UpdateEmail")]
    public IActionResult UpdateUserEmail(int id, string email)
    {
        User? user = _service.GetUserById(id);

        if (user == null)
            return NotFound();

        bool result = _service.UpdateUserEmail(id, email);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpPatch("{id}/password", Name = "UpdatePassword")]
    public IActionResult UpdateUserpassword(int id, string password)
    {
        User? user = _service.GetUserById(id);

        if (user == null)
            return NotFound();

        bool result = _service.UpdateUserPassword(id, password);

        if (!result)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        bool result = _service.DeleteUser(id);

        if (!result)
            return NotFound();

        return Ok();
    }
}
