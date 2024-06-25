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
        public async Task AddAsync_ShouldReturnBankAccountEntity()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = 1000 };
            var bankAccount = new BankAccount { Id = accountId, UserId = userId, Balance = 1000 };
            var exception = new Exception();

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
    }
}