using GenericSmallBusinessApp.Server.AuthenticationAndAuthorization;
using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericSmallBusinessApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService service) : ControllerBase
    {
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await service.GetAllUsersRequest();
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        [ServiceFilter(typeof(IdAuthorizationAttribute))]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await service.GetUserByUserIdRequest(id);
            return Ok(user);
        }

        [HttpPost("AddUser/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser([FromForm] UserDto request, int id)
        {
            var result = await service.AddUserRequest(request);
            return Ok(result);
        }

        [HttpPost("UpdateUser")]
        [ServiceFilter(typeof(IdAuthorizationAttribute))]
        public async Task<ActionResult> UpdateUser([FromForm] UserDto request)
        {
            var result = await service.UpdateUserRequest(request);
            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        [ServiceFilter(typeof(IdAuthorizationAttribute))]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await service.DeleteUserRequeset(id);
            return Ok(result);
        }
    }
}
