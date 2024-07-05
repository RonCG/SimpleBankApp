using ErrorOr;
using FluentAssertions;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using LinqToDB.SqlProvider;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Infrastructure.Persistance;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;
using SimpleBankApp.Infrastructure.Persistance.Repositories;
using SimpleBankApp.Infrastructure.Persistance.Repositories.Common;

namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Infrastructure.Tests.Persistance.Repositories
{
    public class BankAccountRepositoryTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ICommonRepository> _mockDb;
        private readonly BankAccountRepository _repository;

        public BankAccountRepositoryTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockDb = new Mock<ICommonRepository>();
            _repository = new BankAccountRepository(_mockMapper.Object, _mockDb.Object);
        }

        [Fact]
        public async Task BankAccountRepository_WhenBankAccountIsCreated_ReturnsCreatedBankAccountEntity()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = 1000 };
            var bankAccount = new BankAccount { Id = accountId, UserId = userId, Balance = 1000 };

            _mockMapper
                .Setup(mapper => mapper.Map<BankAccount>(It.IsAny<BankAccountEntity>()))
                .Returns(bankAccount);

            _mockMapper
                .Setup(mapper => mapper.Map<BankAccountEntity>(It.IsAny<BankAccount>()))
                .Returns(bankAccountEntity);

            _mockDb
                .Setup(db => db.InsertAsync(bankAccount, false))
                .ReturnsAsync(1);

            // Act
            var result = await _repository.AddAsync(bankAccountEntity);

            // Assert
            result.Should().BeEquivalentTo(bankAccountEntity);
        }

        [Fact]
        public async Task GetBankAccount_WhenBankAccountExists_ReturnsBankAccountEntity()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = 1000 };
            var bankAccount = new BankAccount { Id = accountId, UserId = userId, Balance = 1000 };

            _mockDb
                .Setup(db => db.GetByPredicate<BankAccount>(It.IsAny<Func<BankAccount, bool>>()))
                .Returns(bankAccount);

            _mockMapper
                .Setup(mapper => mapper.Map<BankAccountEntity>(It.IsAny<BankAccount>()))
                .Returns(bankAccountEntity);

            // Act
            var result = await _repository.GetBankAccount(accountId, userId);

            // Assert
            result.Should().BeEquivalentTo(bankAccountEntity);
        }


        [Fact]
        public async Task GetBankAccount_WhenBankAccountDoesNotExists_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = 1000 };
            var bankAccount = new BankAccount { Id = accountId, UserId = userId, Balance = 1000 };

            _mockDb
                .Setup(db => db.GetByPredicate<BankAccount>(It.IsAny<Func<BankAccount, bool>>()))
                .Returns((BankAccount)null);

            // Act
            var result = await _repository.GetBankAccount(accountId, userId);

            // Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task BankAccountRepository_WhenBankAccountIsUpdated_ReturnsUpdatedBankAccountEntity()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var balance = 100;
            var createdOn = DateTime.Now;
            var lastUpdatedOn = DateTime.Now;

            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = balance, CreatedOn = createdOn, LastUpdatedOn = lastUpdatedOn };
            var bankAccount = new BankAccount { Id = accountId, UserId = userId, Balance = balance, CreatedOn = createdOn, LastUpdatedOn = lastUpdatedOn };

            _mockMapper
                .Setup(mapper => mapper.Map<BankAccount>(bankAccountEntity))
                .Returns(bankAccount);

            _mockMapper
                .Setup(mapper => mapper.Map<BankAccountEntity>(bankAccount))
                .Returns(bankAccountEntity);

            _mockDb
                .Setup(db => db.UpdateAsync(bankAccount, false))
                .ReturnsAsync(1);

            // Act
            var result = await _repository.UpdateAsync(bankAccountEntity);

            // Assert
            result.Should().BeEquivalentTo(bankAccountEntity);
        }

        [Fact]
        public async Task BankAccountRepository_WhenBankAccountIsDeleted_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId };
            var bankAccount = new BankAccount { Id = accountId, UserId = userId };
            
            _mockMapper
                .Setup(mapper => mapper.Map<BankAccount>(bankAccountEntity))
                .Returns(bankAccount);

            _mockDb
                .Setup(db => db.DeleteAsync(bankAccount))
                .ReturnsAsync(1);

            // Act
            var result = await _repository.DeleteAsync(bankAccountEntity);

            // Assert
            result.Should().BeTrue();
        }
    }
}