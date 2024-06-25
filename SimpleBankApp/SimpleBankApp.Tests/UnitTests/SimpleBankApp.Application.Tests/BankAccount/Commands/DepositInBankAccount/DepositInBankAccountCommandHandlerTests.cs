using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;

namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Application.Tests.BankAccount.Commands.DepositInBankAccount
{
    public class DepositInBankAccountCommandHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DepositInBankAccountCommandHandler _handler;

        public DepositInBankAccountCommandHandlerTests()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new DepositInBankAccountCommandHandler(_mockBankAccountRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task DepositInBankAccountCommand_WhenAccountBalanceIsUpdated_ReturnsDepositInBankAccountCommandResponse()
        {
            // Arrange
            var userId = Guid.NewGuid(); 
            var accountId = Guid.NewGuid();
            var amountToDeposit = 1000;
            var currentBalance = 500;
            var finalBalance = amountToDeposit + currentBalance;
            var lastUpdatedOn = DateTime.Now;

            var command = new DepositInBankAccountCommand { UserId = userId, AccountId = accountId, AmountToDeposit = amountToDeposit };
            var existingBankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = currentBalance };
            var updatedBankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = finalBalance };
            var bankAccountResponse = new DepositInBankAccountCommandResponse { AccountId = accountId, Balance = finalBalance, LastUpdatedOn = lastUpdatedOn };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(existingBankAccountEntity);
            
            _mockBankAccountRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync(updatedBankAccountEntity);

            _mockMapper
                .Setup(mapper => mapper.Map<DepositInBankAccountCommandResponse>(It.IsAny<BankAccountEntity>()))
                .Returns(bankAccountResponse);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<DepositInBankAccountCommandResponse>();
            result.Value.Should().NotBeNull();
            result.Value.AccountId.Should().Be(accountId);
            result.Value.Balance.Should().Be(finalBalance);
            result.Value.LastUpdatedOn.Should().Be(lastUpdatedOn);

        }

        [Fact]
        public async Task DepositInBankAccountCommand_WhenAccountBalanceIsNotUpdated_ReturnsBankAccountNotUpdatedError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToDeposit = 1000;
            var command = new DepositInBankAccountCommand { UserId = userId, AccountId = accountId, AmountToDeposit = amountToDeposit };
            var error =  Errors.BankAccount.BankAccountNotUpdated;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync(It.IsAny<BankAccountEntity>());

            _mockBankAccountRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync((BankAccountEntity)null);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(error.Code);
            result.FirstError.Description.Should().Be(error.Description);
        }

        [Fact]
        public async Task DepositInBankAccountCommand_WhenAccountIsNotFound_ReturnsBankAccountNotFoundError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToDeposit = 1000;
            var command = new DepositInBankAccountCommand { UserId = userId, AccountId = accountId, AmountToDeposit = amountToDeposit };
            var error = Errors.BankAccount.BankAccountNotFound;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync((BankAccountEntity)null);

            _mockBankAccountRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync((BankAccountEntity)null);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(error.Code);
            result.FirstError.Description.Should().Be(error.Description);
        }
    }
}