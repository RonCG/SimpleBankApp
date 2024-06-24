using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Models;
using SimpleBankApp.Application.Authentication.Services;

namespace SimpleBankApp.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger, 
            IAuthenticationService authenticationService)
        {   
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) 
        {
            ErrorOr<RegisterResult> result = await _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);

            return 
                result.Match(
                    result => Ok(new RegisterResponse
                    {
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Email = result.Email
                    }),
                    errors => Problem(errors)       
                 );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);

            return
               result.Match(
                   result => Ok(new LoginResponse
                   {
                       FirstName = result.FirstName,
                       LastName = result.LastName,
                       Email = result.Email,
                       Token = result.Token
                   }),
                   errors => Problem(errors)
                );
        }

    }
}
