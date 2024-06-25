using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.Requests;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;

namespace SimpleBankApp.Api.Controllers
{
    [Route("simple-bank/account")]
    [AllowAnonymous]
    public class BankAccountController : ApiController
    {
        private readonly ILogger<BankAccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICreateBankAccountCommandHandler _createBankAccountCommandHandler;

        public BankAccountController(
            ILogger<BankAccountController> logger,
            IAuthenticationService authenticationService,
            IMapper mapper,
            ICreateBankAccountCommandHandler createBankAccountCommandHandler,
            IHttpContextService httpContextService)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _createBankAccountCommandHandler = createBankAccountCommandHandler;
            _httpContextService = httpContextService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountRequest request) 
        {
            var createBankAccountCommand = new CreateBankAccountCommand
            {
                UserId = _httpContextService.GetUserId(),
                Balance = request.Balance
            };

            var result = await _createBankAccountCommandHandler.Handle(createBankAccountCommand);
            return result.Match(
                createBankAccountCommandResponse => Ok(_mapper.Map<CreateBankAccountResponse>(createBankAccountCommandResponse)),
                errors => Problem(errors));
        }

    }
}
