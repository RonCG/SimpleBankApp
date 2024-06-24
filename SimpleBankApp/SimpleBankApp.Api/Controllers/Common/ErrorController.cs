using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Services;

namespace SimpleBankApp.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public ErrorController(
            ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            
            //log exception

            return Problem();
        }


    }
}
