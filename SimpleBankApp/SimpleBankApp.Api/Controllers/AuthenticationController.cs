using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Models.Inputs;
using SimpleBankApp.Application.Authentication.Models.Results;
using SimpleBankApp.Application.Authentication.Services;

namespace SimpleBankApp.Api.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthenticationService authenticationService,
            IMapper mapper)
        {
            _logger = logger;   
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest) 
        {
            RegisterInput registerInput = _mapper.Map<RegisterInput>(registerRequest);
            ErrorOr<RegisterResult> result = await _authenticationService.Register(registerInput);

            return result.Match(
                registerResult => Ok(_mapper.Map<RegisterResponse>(registerResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            LoginInput loginInput = _mapper.Map<LoginInput>(loginRequest);
            ErrorOr<LoginResult> result = await _authenticationService.Login(loginInput);

            return result.Match(
                loginResult => Ok(_mapper.Map<LoginResponse>(loginResult)),
                errors => Problem(errors));
        }

    }
}
