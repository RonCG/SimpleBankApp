using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Contracts.Authentication.Request;
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
        public IActionResult Register([FromBody] RegisterRequest registerRequest) 
        {   
            return Ok(_authenticationService.Register(registerRequest.firstName, registerRequest.lastName, registerRequest.email, registerRequest.password));
        }

    }
}
