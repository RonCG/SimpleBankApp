using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Services;

namespace SimpleBankApp.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public ApiController(
            ILogger<AuthenticationController> logger, 
            IAuthenticationService authenticationService)
        {
            _logger = logger;
        }


    }
}
