using ErrorOr;
using FluentAssertions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount;
using SimpleBankApp.Api.Controllers;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;
using SimpleBankApp.Domain.Common.Errors;
using System.Net;


namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Api.Tests.Controllers.BankAccount
{
    public class BankAccountControllerWithdrawFromTests
    {
        private readonly BankAccountController _controller;

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IMediator> _mockMediator;

        public BankAccountControllerWithdrawFromTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextService = new Mock<IHttpContextService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockMediator = new Mock<IMediator>();

            _controller = new BankAccountController(
                new Mock<ILogger<BankAccountController>>().Object,
                _mockMapper.Object,
                _mockHttpContextService.Object,
                _mockAuthenticationService.Object,
                _mockMediator.Object);
        }


        [Fact]
        public async Task WithdrawFromBankAccount_WhenDataIsValid_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToWithdraw = 1000;
            var currentBalance = 5000;
            var finalBalance = currentBalance - amountToWithdraw;
            var lastUpdatedOn = DateTime.Now;
            var request = new WithdrawFromBankAccountRequest { AccountId = accountId, AmountToWithdraw = amountToWithdraw };
            var commandResponse = new WithdrawFromBankAccountCommandResponse { AccountId = accountId, Balance = finalBalance, LastUpdatedOn = lastUpdatedOn };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockMediator.Setup(m => m.Send(It.IsAny<WithdrawFromBankAccountCommand>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<WithdrawFromBankAccountResponse>(It.IsAny<WithdrawFromBankAccountCommandResponse>()))
                .Returns(new WithdrawFromBankAccountResponse { AccountId = accountId, Balance = finalBalance, LastUpdatedOn = lastUpdatedOn });

            // Act
            var result = await _controller.WithdrawFromBankAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<WithdrawFromBankAccountResponse>(okResult.Value);
            returnValue.AccountId.Should().Be(accountId);
            returnValue.LastUpdatedOn.Should().Be(lastUpdatedOn);
            returnValue.Balance.Should().Be(finalBalance);

        }

        [Fact]
        public async Task WithdrawFromBankAccount_WhenDataIsNotValid_Returns500StatusCode()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToWithdraw = 1000;

            var request = new WithdrawFromBankAccountRequest { AccountId = accountId, AmountToWithdraw = amountToWithdraw };
            
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotUpdated;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockMediator.Setup(m => m.Send(It.IsAny<WithdrawFromBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<WithdrawFromBankAccountResponse>(It.IsAny<WithdrawFromBankAccountCommandResponse>()))
                .Returns(It.IsAny<WithdrawFromBankAccountResponse>());

            // Act
            var result = await _controller.WithdrawFromBankAccount(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            errorResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
