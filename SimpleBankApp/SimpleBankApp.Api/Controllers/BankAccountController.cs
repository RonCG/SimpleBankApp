using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DeleteBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.GetBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;

namespace SimpleBankApp.Api.Controllers
{
    [Route("simple-bank/account")]
    public class BankAccountController : ApiController
    {
        private readonly ILogger<BankAccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;

        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public BankAccountController(
            ILogger<BankAccountController> logger,
            IMapper mapper,
            IHttpContextService httpContextService,
            IAuthenticationService authenticationService,
            IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _httpContextService = httpContextService;
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountRequest request) 
        {
            var createBankAccountCommand = new CreateBankAccountCommand
            {
                UserId = _httpContextService.GetUserId(),
                Balance = request.Balance
            };

            var result = await _mediator.Send(createBankAccountCommand);
            return result.Match(
                createBankAccountCommandResponse => Ok(_mapper.Map<CreateBankAccountResponse>(createBankAccountCommandResponse)),
                errors => Problem(errors));
        }


        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetBankAccount([FromRoute] Guid accountId)
        {
            var getBankAccountCommand = new GetBankAccountQuery
            {
                UserId = _httpContextService.GetUserId(),
                AccountId = accountId
            };

            var result = await _mediator.Send(getBankAccountCommand);
            return result.Match(
                getBankAccountCommandResponse => Ok(_mapper.Map<GetBankAccountResponse>(getBankAccountCommandResponse)),
                errors => Problem(errors));
        }


        [HttpPost("deposit")]
        public async Task<IActionResult> DepositInBankAccount([FromBody] DepositInBankAccountRequest request)
        {
            var depositInBankAccountCommand = new DepositInBankAccountCommand
            {
                UserId = _httpContextService.GetUserId(),
                AccountId = request.AccountId,
                AmountToDeposit = request.AmountToDeposit
            };

            var result = await _mediator.Send(depositInBankAccountCommand);
            return result.Match(
                depositInBankAccountCommandResponse => Ok(_mapper.Map<DepositInBankAccountResponse>(depositInBankAccountCommandResponse)),
                errors => Problem(errors));
        }


        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawFromBankAccount([FromBody] WithdrawFromBankAccountRequest request)
        {
            var withdrawFromBankAccountCommand = new WithdrawFromBankAccountCommand
            {
                UserId = _httpContextService.GetUserId(),
                AccountId = request.AccountId,
                AmountToWithdraw = request.AmountToWithdraw
            };

            var result = await _mediator.Send(withdrawFromBankAccountCommand);
            return result.Match(
                withdrawFromBankAccountCommandResponse => Ok(_mapper.Map<WithdrawFromBankAccountResponse>(withdrawFromBankAccountCommandResponse)),
                errors => Problem(errors));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBankAccount([FromBody] DeleteBankAccountRequest request)
        {
            var deleteBankAccountCommand = new DeleteBankAccountCommand
            {
                UserId = _httpContextService.GetUserId(),
                AccountId = request.AccountId
            };

            var result = await _mediator.Send(deleteBankAccountCommand);
            return result.Match(
                deleteBankAccountCommandResponse => Ok(_mapper.Map<DeleteBankAccountResponse>(deleteBankAccountCommandResponse)),
                errors => Problem(errors));
        }

    }
}
