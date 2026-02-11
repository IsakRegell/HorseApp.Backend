using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorseApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth-debug")]
    public class AuthDebugController : ControllerBase
    {
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(claims);
        }
    }
}
