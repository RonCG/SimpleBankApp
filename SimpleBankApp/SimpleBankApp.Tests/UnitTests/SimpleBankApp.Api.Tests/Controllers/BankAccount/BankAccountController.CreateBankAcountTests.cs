using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Controllers;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Domain.Common.Errors;


namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Api.Tests.Controllers.BankAccount
{
    public class BankAccountControllerCreateBankAccountTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly BankAccountController _controller;

        private readonly Mock<ICreateBankAccountCommandHandler> _mockCreateBankAccountCommandHandler;
        private readonly Mock<IDepositInBankAccountCommandHandler> _mockDepositInBankAccountCommandHandler;
        private readonly Mock<IWithdrawFromBankAccountCommandHandler> _mockWithdrawFromBankAccountCommandHandler;
        private readonly Mock<IDeleteBankAccountCommandHandler> _mockDeleteBankAccountCommandHandler;

        public BankAccountControllerCreateBankAccountTests()
        {
            _mockCreateBankAccountCommandHandler = new Mock<ICreateBankAccountCommandHandler>();
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
                _mockDepositInBankAccountCommandHandler.Object,
                _mockWithdrawFromBankAccountCommandHandler.Object,
                _mockDeleteBankAccountCommandHandler.Object);
        }


        [Fact]
        public async Task CreateBankAccount_WhenAccountIsCreated_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var request = new CreateBankAccountRequest { Balance = 1000 };
            var command = new CreateBankAccountCommand { UserId = userId, Balance = 1000 };
            var commandResponse = new CreateBankAccountCommandResponse { Id = accountId, UserId = userId, Balance = 1000 };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockCreateBankAccountCommandHandler
                .Setup(handler => handler.Handle(command))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<CreateBankAccountResponse>(It.IsAny<CreateBankAccountCommandResponse>()))
                .Returns(new CreateBankAccountResponse { Id = accountId, UserId = userId, Balance = 1000 });

            // Act
            var result = await _controller.CreateBankAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateBankAccountResponse>(okResult.Value);
            returnValue.Id.Should().Be(accountId);
            returnValue.UserId.Should().Be(userId);
            returnValue.Balance.Should().Be(1000);

        }

        [Fact]
        public async Task CreateBankAccount_WhenAccountIsNotCreated_ReturnsObjectResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var request = new CreateBankAccountRequest { Balance = 1000 };
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotCreated;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockCreateBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<CreateBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<CreateBankAccountResponse>(It.IsAny<CreateBankAccountCommandResponse>()))
                .Returns(It.IsAny<CreateBankAccountResponse>);

            // Act
            var result = await _controller.CreateBankAccount(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);

        }

    }
}
