using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBankApp.Api.Common.Http;
using SimpleBankApp.Api.Contracts.BankAccount.DeleteBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount;
using SimpleBankApp.Api.Controllers;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Domain.Common.Errors;


namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Api.Tests.Controllers.BankAccount
{
    public class BankAccountControllerDeleteTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextService> _mockHttpContextService;
        private readonly BankAccountController _controller;

        private readonly Mock<ICreateBankAccountCommandHandler> _mockCreateBankAccountCommandHandler;
        private readonly Mock<IDepositInBankAccountCommandHandler> _mockDepositInBankAccountCommandHandler;
        private readonly Mock<IWithdrawFromBankAccountCommandHandler> _mockWithdrawFromBankAccountCommandHandler;
        private readonly Mock<IDeleteBankAccountCommandHandler> _mockDeleteBankAccountCommandHandler;

        public BankAccountControllerDeleteTests()
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
        public async Task DeleteBankAccount_WhenDataIsValid_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var request = new DeleteBankAccountRequest { AccountId = accountId};
            var commandResponse = new DeleteBankAccountCommandResponse { IsDeleted = true };

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockDeleteBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<DeleteBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<DeleteBankAccountResponse>(It.IsAny<DeleteBankAccountCommandResponse>()))
                .Returns(new DeleteBankAccountResponse { IsDeleted = true });

            // Act
            var result = await _controller.DeleteBankAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DeleteBankAccountResponse>(okResult.Value);
            returnValue.IsDeleted.Should().BeTrue();

        }

        [Fact]
        public async Task DeleteBankAccount_WhenDataIsNotValid_ReturnsObjectResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            var request = new DeleteBankAccountRequest { AccountId = accountId };
            
            ErrorOr.Error commandResponse = Errors.BankAccount.BankAccountNotDeleted;

            _mockHttpContextService
                .Setup(service => service.GetUserId())
                .Returns(userId);

            _mockDeleteBankAccountCommandHandler
                .Setup(handler => handler.Handle(It.IsAny<DeleteBankAccountCommand>()))
                .ReturnsAsync(commandResponse);

            _mockMapper
                .Setup(mapper => mapper.Map<DeleteBankAccountResponse>(It.IsAny<DeleteBankAccountCommandResponse>()))
                .Returns(It.IsAny<DeleteBankAccountResponse>());

            // Act
            var result = await _controller.DeleteBankAccount(request);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);

        }
    }
}
