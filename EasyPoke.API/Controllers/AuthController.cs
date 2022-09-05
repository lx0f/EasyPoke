using EasyPoke.API.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoke.API.Controllers;

[ApiKeyAuth]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAuthCheck()
    {
        return Ok();
    }
}
