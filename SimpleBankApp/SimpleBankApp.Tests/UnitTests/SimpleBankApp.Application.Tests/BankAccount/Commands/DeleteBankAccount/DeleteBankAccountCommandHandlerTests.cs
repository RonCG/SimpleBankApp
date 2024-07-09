using FluentAssertions;
using MapsterMapper;
using Moq;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;

namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Application.Tests.BankAccount.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommandHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DeleteBankAccountCommandHandler _handler;

        public DeleteBankAccountCommandHandlerTests()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new DeleteBankAccountCommandHandler(_mockBankAccountRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task DeleteBankAccountCommand_WhenAccountIsDeleted_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid(); 
            var accountId = Guid.NewGuid();

            var command = new DeleteBankAccountCommand { UserId = userId, AccountId = accountId };
            var existingBankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(existingBankAccountEntity);
            
            _mockBankAccountRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync(true);

 
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<DeleteBankAccountCommandResponse>();
            result.Value.Should().NotBeNull();
            result.Value.IsDeleted.Should().BeTrue();

        }

        [Fact]
        public async Task DeleteBankAccountCommand_WhenAccountIsNotDeleted_ReturnsBankAccountNotDeletedError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var bankAccount = new BankAccountEntity { Id = accountId, UserId = userId };
            var command = new DeleteBankAccountCommand { UserId = userId, AccountId = accountId };
            var error =  Errors.BankAccount.BankAccountNotDeleted;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync(bankAccount);

            _mockBankAccountRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(error.Code);
            result.FirstError.Description.Should().Be(error.Description);
        }

        [Fact]
        public async Task DeleteBankAccountCommand_WhenAccountIsNotFound_ReturnsBankAccountNotFoundError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var command = new DeleteBankAccountCommand { UserId = userId, AccountId = accountId };
            var error = Errors.BankAccount.BankAccountNotFound;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync((BankAccountEntity)null);

            _mockBankAccountRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync((BankAccountEntity)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(error.Code);
            result.FirstError.Description.Should().Be(error.Description);
        }

        [Fact]
        public async Task DeleteBankAccountCommand_WhenAccountBalanceIsGreaterThanZero_ReturnsBankAccountCannotDeleteWithAvailableFundsError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var currentBalance = 1000;
            var bankAccount = new BankAccountEntity { Id = accountId, Balance = currentBalance };
            var command = new DeleteBankAccountCommand { UserId = userId, AccountId = accountId };
            var error = Errors.BankAccount.CannotDeleteWithAvailableFunds;

            _mockBankAccountRepository
               .Setup(repo => repo.GetBankAccount(It.IsAny<Guid>(), It.IsAny<Guid>()))
               .ReturnsAsync(bankAccount);

            _mockBankAccountRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(error.Code);
            result.FirstError.Description.Should().Be(error.Description);
        }
    }
}