using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.DTOs;
using RapidPay.Application.Interfaces.Infrastructure;

namespace RapidPay.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            LoginResponseDto loginResponse = await _authService.Login(request);
            if (!loginResponse.Succeeded)
                return BadRequest(loginResponse.Succeeded);

            return Ok(loginResponse.Token);
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register(RegistrationDto request)
        {
            RegistrationResponseDto registrationResponse = await _authService.Registration(request);
            if (!registrationResponse.Succeeded)
                return BadRequest(registrationResponse.Message);

            return Ok();
        }
    }
}
