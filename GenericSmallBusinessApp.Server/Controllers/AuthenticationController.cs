using GenericSmallBusinessApp.Server.AuthenticationAndAuthorization;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericSmallBusinessApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ILoginAuthentication authentication) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromForm] LoginDto request)
        {
            var user = await authentication.LoginRequest(request);
            return Ok(user);
        }
    }
}
