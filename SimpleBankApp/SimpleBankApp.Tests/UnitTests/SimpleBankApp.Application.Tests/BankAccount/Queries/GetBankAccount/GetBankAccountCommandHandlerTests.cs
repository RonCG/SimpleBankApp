using FluentAssertions;
using MapsterMapper;
using Moq;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;

namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Application.Tests.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountCommandHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetBankAccountCommandHandler _handler;

        public GetBankAccountCommandHandlerTests()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetBankAccountCommandHandler(_mockBankAccountRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetBankAccountCommand_WhenAccountExists_ReturnsGetBankAccountCommandResponse()
        {
            // Arrange
            var userId = Guid.NewGuid(); 
            var accountId = Guid.NewGuid();
            var balance = 1000;
            var lastUpdatedOn = DateTime.Now;
            var command = new GetBankAccountCommand { UserId = userId, AccountId = accountId };
            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = balance };
            var bankAccountResponse = new GetBankAccountCommandResponse { AccountId = accountId, Balance = balance, LastUpdatedOn = lastUpdatedOn };

            _mockBankAccountRepository
                .Setup(repo => repo.GetBankAccount(accountId, userId))
                .ReturnsAsync(bankAccountEntity);

            _mockMapper
                .Setup(mapper => mapper.Map<GetBankAccountCommandResponse>(It.IsAny<BankAccountEntity>()))
                .Returns(bankAccountResponse);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<GetBankAccountCommandResponse>();
            result.Value.Should().NotBeNull();
            result.Value.AccountId.Should().Be(accountId);
            result.Value.Balance.Should().Be(balance);
            result.Value.LastUpdatedOn.Should().Be(lastUpdatedOn);
        }

        [Fact]
        public async Task GetBankAccountCommand_WhenAccountDoesNotExists_ReturnsBankAccountNotFoundError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var command = new GetBankAccountCommand { UserId = userId, AccountId = accountId };
            var error =  Errors.BankAccount.BankAccountNotFound;

            _mockBankAccountRepository
                .Setup(repo => repo.AddAsync(It.IsAny<BankAccountEntity>()))
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