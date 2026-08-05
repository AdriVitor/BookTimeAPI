using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService_Application.DTOs;
using UserService_Application.Services.Interfaces;

namespace UserService_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _rolesService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolesService.GetAllAsync();
            return Ok(roles);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RoleDTO roledto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _rolesService.CreateAsync(roledto);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] RoleDTO roleDTO)
        {
            await _rolesService.UpdateAsync(roleDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _rolesService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }

}
