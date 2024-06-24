using Microsoft.AspNetCore.Mvc;

namespace SimpleBankApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test")]
        public IActionResult GetTest() 
        {
            return Ok(new List<int> { 1, 2, 3});
        }

    }
}
