using ErrorOr;
using FluentAssertions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.GetBankAccount;
using SimpleBankApp.Api.Controllers;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;
using SimpleBankApp.Domain.Common.Errors;
using System.Net;


namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Api.Tests.Controllers.BankAccount
{
    public class BankAccountControllerGetBankAccountTests
    {
        private readonly BankAccountController _controller;

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly Mock<IAuthenticationService> _mockAuthenticationService;
        private readonly Mock<IMediator> _mockMediator;

        public BankAccountControllerGetBankAccountTests()
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
        public async Task GetBankAccount_WhenAccountIsFound_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var balance = 100;
            var lastUpdatedOn = DateTime.UtcNow;
            var commandResponse = new GetBankAccountQueryResponse { AccountId = accountId, Balance = balance, LastUpdatedOn = lastUpdatedOn };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockMediator.Setup(m => m.Send(It.IsAny<GetBankAccountQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<GetBankAccountResponse>(commandResponse))
                .Returns(new GetBankAccountResponse { AccountId = commandResponse.AccountId, Balance = commandResponse.Balance, LastUpdatedOn = commandResponse.LastUpdatedOn });

            // Act
            var result = await _controller.GetBankAccount(accountId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GetBankAccountResponse>(okResult.Value);
            returnValue.AccountId.Should().Be(accountId);
            returnValue.Balance.Should().Be(balance);
            returnValue.LastUpdatedOn.Should().Be(lastUpdatedOn);

        }

        [Fact]
        public async Task GetBankAccount_WhenAccountIsNotFound_Returns404StatusCode()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var lastUpdatedOn = DateTime.UtcNow;
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotFound;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockMediator.Setup(m => m.Send(It.IsAny<GetBankAccountQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<GetBankAccountResponse>(It.IsAny<GetBankAccountQueryResponse>()))
                .Returns(new GetBankAccountResponse());

            // Act
            var result = await _controller.GetBankAccount(accountId);
            
            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            errorResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        }

    }
}
