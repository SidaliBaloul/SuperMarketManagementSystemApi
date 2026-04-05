using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarketManagementSystemApi.DTOs;

namespace SuperMarketManagementSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _User;

        public UserController(IUserService user)
        {
            _User = user;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            List<Userr> users = await _User.GetUsersAsync();

            if (users.Count == 0)
                return NotFound("No User Found! ");

            var dto = users.Select(u => new UserDto { UserId = u.UserId, UserName = u.UserName });

            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Userr>> GetUserById(int id, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(User, id, "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            try
            {
                
                Userr user = await _User.GetUserById(id);

                UserDto dto = new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName
                };

                return Ok(dto);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}
