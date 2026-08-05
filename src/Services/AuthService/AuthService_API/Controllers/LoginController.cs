using AuthService_API.DTOs;
using AuthService_API.Services;
using AuthService_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> Post([FromBody] LoginRequestDTO requestDTO)
        {
            try
            {
                var loginResponse = await _loginService.GenerateToken(requestDTO);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
