using ErrorOr;
using FluentAssertions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Controllers;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;
using SimpleBankApp.Domain.Common.Errors;
using System.Net;


namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Api.Tests.Controllers.BankAccount
{
    public class BankAccountControllerCreateBankAccountTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly BankAccountController _controller;

        private readonly Mock<IMediator> _mockMediator;

        private readonly Mock<IGetBankAccountCommandHandler> _mockGetBankAccountCommandHandler;

        public BankAccountControllerCreateBankAccountTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockGetBankAccountCommandHandler = new Mock<IGetBankAccountCommandHandler>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextService = new Mock<IHttpContextService>();

            _controller = new BankAccountController(
                new Mock<ILogger<BankAccountController>>().Object,
                _mockMapper.Object,
                _mockHttpContextService.Object,
                new Mock<IAuthenticationService>().Object,
                _mockMediator.Object,
                _mockGetBankAccountCommandHandler.Object);
        }


        [Fact]
        public async Task CreateBankAccount_WhenAccountIsCreated_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var balance = 1000;
            var request = new CreateBankAccountRequest { Balance = balance };
            var commandResponse = new CreateBankAccountCommandResponse { Id = accountId, UserId = userId, Balance = balance };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()))
                  .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<CreateBankAccountResponse>(It.IsAny<CreateBankAccountCommandResponse>()))
                .Returns(new CreateBankAccountResponse { AccountId = accountId, UserId = userId, Balance = 1000 });

            // Act
            var result = await _controller.CreateBankAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateBankAccountResponse>(okResult.Value);
            returnValue.AccountId.Should().Be(accountId);
            returnValue.UserId.Should().Be(userId);
            returnValue.Balance.Should().Be(balance);

        }

        [Fact]
        public async Task CreateBankAccount_WhenAccountIsNotCreated_Returns500StatusCode()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var request = new CreateBankAccountRequest { Balance = 1000 };
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotCreated;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<CreateBankAccountResponse>(It.IsAny<CreateBankAccountCommandResponse>()))
                .Returns(It.IsAny<CreateBankAccountResponse>);

            // Act
            var result = await _controller.CreateBankAccount(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            errorResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

        }

    }
}
