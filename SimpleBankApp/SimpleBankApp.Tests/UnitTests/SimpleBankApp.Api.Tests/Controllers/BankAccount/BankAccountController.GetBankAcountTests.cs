using ErrorOr;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.GetBankAccount;
using SimpleBankApp.Api.Controllers;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;
using SimpleBankApp.Domain.Common.Errors;
using System.Net;


namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Api.Tests.Controllers.BankAccount
{
    public class BankAccountControllerGetBankAccountTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly BankAccountController _controller;

        private readonly Mock<ICreateBankAccountCommandHandler> _mockCreateBankAccountCommandHandler;
        private readonly Mock<IGetBankAccountCommandHandler> _mockGetBankAccountCommandHandler;
        private readonly Mock<IDepositInBankAccountCommandHandler> _mockDepositInBankAccountCommandHandler;
        private readonly Mock<IWithdrawFromBankAccountCommandHandler> _mockWithdrawFromBankAccountCommandHandler;
        private readonly Mock<IDeleteBankAccountCommandHandler> _mockDeleteBankAccountCommandHandler;

        public BankAccountControllerGetBankAccountTests()
        {
            _mockCreateBankAccountCommandHandler = new Mock<ICreateBankAccountCommandHandler>();
            _mockGetBankAccountCommandHandler = new Mock<IGetBankAccountCommandHandler>();
            _mockDepositInBankAccountCommandHandler = new Mock<IDepositInBankAccountCommandHandler>();
            _mockWithdrawFromBankAccountCommandHandler = new Mock<IWithdrawFromBankAccountCommandHandler>();
            _mockDeleteBankAccountCommandHandler = new Mock<IDeleteBankAccountCommandHandler>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextService = new Mock<IHttpContextService>();

            _controller = new BankAccountController(
                new Mock<ILogger<BankAccountController>>().Object,
                _mockMapper.Object,
                _mockHttpContextService.Object,
                new Mock<IAuthenticationService>().Object,
                _mockCreateBankAccountCommandHandler.Object,
                _mockGetBankAccountCommandHandler.Object,
                _mockDepositInBankAccountCommandHandler.Object,
                _mockWithdrawFromBankAccountCommandHandler.Object,
                _mockDeleteBankAccountCommandHandler.Object);
        }


        [Fact]
        public async Task GetBankAccount_WhenAccountIsFound_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var balance = 100;
            var lastUpdatedOn = DateTime.UtcNow;
            var request = new GetBankAccountRequest { AccountId = accountId };
            var commandResponse = new GetBankAccountCommandResponse { Id = accountId, Balance = balance, LastUpdatedOn = lastUpdatedOn };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockGetBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<GetBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<GetBankAccountResponse>(commandResponse))
                .Returns(new GetBankAccountResponse { AccountId = commandResponse.Id, Balance = commandResponse.Balance, LastUpdatedOn = commandResponse.LastUpdatedOn });

            // Act
            var result = await _controller.GetBankAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetBankAccountResponse>(okResult.Value);
            returnValue.AccountId.Should().Be(accountId);
            returnValue.Balance.Should().Be(balance);
            returnValue.LastUpdatedOn.Should().Be(lastUpdatedOn);

        }

        [Fact]
        public async Task GetBankAccount_WhenAccountIsNotFound_ReturnsObjectResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var lastUpdatedOn = DateTime.UtcNow;
            var request = new GetBankAccountRequest { AccountId = accountId };
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotFound;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockGetBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<GetBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<GetBankAccountResponse>(It.IsAny<GetBankAccountCommandResponse>()))
                .Returns(new GetBankAccountResponse());

            // Act
            var result = await _controller.GetBankAccount(request);
            
            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            errorResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }

    }
}
