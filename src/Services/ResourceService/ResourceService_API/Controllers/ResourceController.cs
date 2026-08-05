using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceService_Application.DTOs.Resources;
using ResourceService_Application.Services.Interfaces;
using ResourceService_Domain.Entities;

namespace ResourceService_API.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        // GET: api/resource
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetAllAsync()
        {
            var resources = await _resourceService.GetAllAsync();

            return Ok(resources);
        }

        // GET: api/resource/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Resource>> GetByIdAsync(int id)
        {
            var resource = await _resourceService.GetByIdAsync(id);
            if (resource == null)
                return NotFound(new { message = "Resource not found" });

            return Ok(resource);
        }

        // POST: api/resource
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ResourceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _resourceService.CreateAsync(dto);

            return Ok();
        }

        // PUT: api/resource/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ResourceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _resourceService.UpdateAsync(dto);

            return NoContent();
        }

        // DELETE: api/resource/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _resourceService.DeleteAsync(id);
            return Ok();
        }
    }
}
