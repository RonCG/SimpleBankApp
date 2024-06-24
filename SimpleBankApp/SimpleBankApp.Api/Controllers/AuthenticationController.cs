using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Services;

namespace SimpleBankApp.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) 
        {
            var result = await _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);
            var response = new RegisterResponse
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email
            };

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            var response = new LoginResponse
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Token = result.Token
            };

            return Ok(response);
        }

    }
}
