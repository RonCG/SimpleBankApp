using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;

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
        private readonly IDepositInBankAccountCommandHandler _depositInBankAccountCommandHandler;

        public BankAccountController(
            ILogger<BankAccountController> logger,
            IMapper mapper,
            IHttpContextService httpContextService,
            IAuthenticationService authenticationService,
            ICreateBankAccountCommandHandler createBankAccountCommandHandler,
            IDepositInBankAccountCommandHandler depositInBankAccountCommandHandler)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextService = httpContextService;
            _authenticationService = authenticationService;
            _createBankAccountCommandHandler = createBankAccountCommandHandler;
            _depositInBankAccountCommandHandler = depositInBankAccountCommandHandler;
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

        [HttpPost("/deposit")]
        public async Task<IActionResult> DepositInBankAccount([FromBody] DepositInBankAccountRequest request)
        {
            var depositInBankAccountCommand = new DepositInBankAccountCommand
            {
                UserId = _httpContextService.GetUserId(),
                AmountToDeposit = request.AmountToDeposit
            };

            var result = await _depositInBankAccountCommandHandler.Handle(depositInBankAccountCommand);
            return result.Match(
                depositInBankAccountCommandResponse => Ok(_mapper.Map<DepositInBankAccountResponse>(depositInBankAccountCommandResponse)),
                errors => Problem(errors));
        }

    }
}
