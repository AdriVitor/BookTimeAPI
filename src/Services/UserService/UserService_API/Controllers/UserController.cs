using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService_Application.DTOs.Users;
using UserService_Application.Services.Interfaces;
using UserService_Domain.Entities;

namespace YourProject.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("Exists")]
        [AllowAnonymous]
        public async Task<ActionResult<UserExistsResponseDTO>> IsExists([FromBody] UserExistsRequestDTO dto)
        {
            try
            {
                var userExists = new UserExistsResponseDTO()
                {
                    Id = await _userService.IsExists(dto.Email, dto.Password)
                };

                return Ok(userExists);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(UserDTO userDTO)
        {
            try
            {
                await _userService.CreateAsync(userDTO);
                return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult<User>> Update(int id, User user)
        {
            try
            {
                await _userService.UpdateAsync(user);
                return Ok();
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}
