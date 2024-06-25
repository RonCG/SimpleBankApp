using FluentAssertions;
using MapsterMapper;
using Moq;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;

namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Application.Tests.BankAccount.Commands.DepositInBankAccount
{
    public class WithdrawFromBankAccountCommandHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly WithdrawFromBankAccountCommandHandler _handler;

        public WithdrawFromBankAccountCommandHandlerTests()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new WithdrawFromBankAccountCommandHandler(_mockBankAccountRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task WithdrawFromBankAccountCommand_WhenAccountBalanceIsUpdated_ReturnsWithdrawFromBankAccountCommandResponse()
        {
            // Arrange
            var userId = Guid.NewGuid(); 
            var accountId = Guid.NewGuid();
            var amountToWithdraw = 1000;
            var currentBalance = 5000;
            var finalBalance = currentBalance - amountToWithdraw;
            var lastUpdatedOn = DateTime.Now;

            var command = new WithdrawFromBankAccountCommand { UserId = userId, AccountId = accountId, AmountToWithdraw = amountToWithdraw };
            var existingBankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = currentBalance };
            var updatedBankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = finalBalance };
            var bankAccountResponse = new WithdrawFromBankAccountCommandResponse { AccountId = accountId, Balance = finalBalance, LastUpdatedOn = lastUpdatedOn };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(existingBankAccountEntity);
            
            _mockBankAccountRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync(updatedBankAccountEntity);

            _mockMapper
                .Setup(mapper => mapper.Map<WithdrawFromBankAccountCommandResponse>(It.IsAny<BankAccountEntity>()))
                .Returns(bankAccountResponse);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<WithdrawFromBankAccountCommandResponse>();
            result.Value.Should().NotBeNull();
            result.Value.AccountId.Should().Be(accountId);
            result.Value.Balance.Should().Be(finalBalance);
            result.Value.LastUpdatedOn.Should().Be(lastUpdatedOn);

        }

        [Fact]
        public async Task WithdrawFromBankAccountCommand_WhenAccountBalanceIsNotUpdated_ReturnsBankAccountNotUpdatedError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToWithdraw = 1000;
            var currentBalance = amountToWithdraw * 2;
            var bankAccount = new BankAccountEntity { Id = accountId, Balance = currentBalance };
            var command = new WithdrawFromBankAccountCommand { UserId = userId, AccountId = accountId, AmountToWithdraw = amountToWithdraw };
            var error =  Errors.BankAccount.BankAccountNotUpdated;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync(bankAccount);

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
            var amountToWithdraw = 1000;
            var command = new WithdrawFromBankAccountCommand { UserId = userId, AccountId = accountId, AmountToWithdraw = amountToWithdraw };
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

        [Fact]
        public async Task WithdrawFromBankAccountCommand_WhenAccountBalanceIsInssuficient_ReturnsBankAccountInsufficientFundsError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var amountToWithdraw = 1000;
            var currentBalance = amountToWithdraw/2;
            var bankAccount = new BankAccountEntity { Id = accountId, Balance = currentBalance };
            var command = new WithdrawFromBankAccountCommand { UserId = userId, AccountId = accountId, AmountToWithdraw = amountToWithdraw };
            var error = Errors.BankAccount.InsufficientFundsForWithdraw;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync(bankAccount);

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