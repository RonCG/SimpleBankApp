using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount;
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
    public class BankAccountControllerDepositInTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly BankAccountController _controller;

        private readonly Mock<ICreateBankAccountCommandHandler> _mockCreateBankAccountCommandHandler;
        private readonly Mock<IGetBankAccountCommandHandler> _mockGetBankAccountCommandHandler;
        private readonly Mock<IDepositInBankAccountCommandHandler> _mockDepositInBankAccountCommandHandler;
        private readonly Mock<IWithdrawFromBankAccountCommandHandler> _mockWithdrawFromBankAccountCommandHandler;
        private readonly Mock<IDeleteBankAccountCommandHandler> _mockDeleteBankAccountCommandHandler;

        public BankAccountControllerDepositInTests()
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
        public async Task DepositInBankAccount_WhenDataIsValid_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToDeposit = 1000;
            var currentBalance = 500;
            var finalBalance = amountToDeposit + currentBalance;
            var lastUpdatedOn = DateTime.Now;
            var request = new DepositInBankAccountRequest { AccountId = accountId, AmountToDeposit = amountToDeposit };
            var commandResponse = new DepositInBankAccountCommandResponse { AccountId = accountId, Balance = finalBalance, LastUpdatedOn = lastUpdatedOn };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockDepositInBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<DepositInBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<DepositInBankAccountResponse>(It.IsAny<DepositInBankAccountCommandResponse>()))
                .Returns(new DepositInBankAccountResponse { AccountId = accountId, Balance = amountToDeposit + currentBalance, LastUpdatedOn = lastUpdatedOn });

            // Act
            var result = await _controller.DepositInBankAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DepositInBankAccountResponse>(okResult.Value);
            returnValue.AccountId.Should().Be(accountId);
            returnValue.LastUpdatedOn.Should().Be(lastUpdatedOn);
            returnValue.Balance.Should().Be(finalBalance);

        }

        [Fact]
        public async Task DepositInBankAccount_WhenDataIsNotValid_Returns500StatusCode()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToDeposit = 1000;

            var request = new DepositInBankAccountRequest { AccountId = accountId, AmountToDeposit = amountToDeposit };
            
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotUpdated;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockDepositInBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<DepositInBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<DepositInBankAccountResponse>(It.IsAny<DepositInBankAccountCommandResponse>()))
                .Returns(It.IsAny<DepositInBankAccountResponse>());

            // Act
            var result = await _controller.DepositInBankAccount(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            errorResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

        }
    }
}
