using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService_Application.DTOs.UserRoles;
using UserService_Application.Services.Interfaces;

namespace UserRoles.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _service;

        public UserRolesController(IUserRolesService service)
        {
            _service = service;
        }

        /// <summary>
        /// Buscar todos os relacionamentos UserRoles
        /// </summary>
        [HttpGet("All/{userId}")]
        public async Task<ActionResult<IEnumerable<UserService_Domain.Entities.UserRoles>>> GetAll(int userId)
        {
            return Ok(await _service.GetAllByUser(userId));
        }

        /// <summary>
        /// Buscar relacionamento específico pelo par (UserId, RoleId)
        /// </summary>
        [HttpGet("{userId:int}/{roleId:int}")]
        public async Task<ActionResult<UserService_Domain.Entities.UserRoles>> Get(int userId, int roleId)
        {
            var result = await _service.GetAsync(userId, roleId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Inserir um novo relacionamento UserRole
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserRolesDTO userRole)
        {
            await _service.AddAsync(userRole);
            return CreatedAtAction(nameof(Get), new { userId = userRole.IdUser, roleId = userRole.IdRole }, userRole);
        }

        /// <summary>
        /// Deletar um relacionamento UserRole
        /// </summary>
        [HttpDelete("{userId:int}/{roleId:int}")]
        public async Task<ActionResult> Delete(int userId, int roleId)
        {
            await _service.DeleteAsync(userId, roleId);
            return Ok();
        }
    }
}
