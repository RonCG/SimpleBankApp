using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleBankApp.Api.Contracts.BankAccount.Requests;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Domain.Common.Errors;

namespace SimpleBankApp.Tests.UnitTests.SimpleBankApp.Application.Tests.BankAccount.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandlerTests
    {
        private readonly Mock<IBankAccountRepository> _mockBankAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateBankAccountCommandHandler _handler;

        public CreateBankAccountCommandHandlerTests()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateBankAccountCommandHandler(_mockBankAccountRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateBankAccountCommand_WhenAccountIsCreated_ReturnsCreateBankAccountCommandResponse()
        {
            // Arrange
            var userId = Guid.NewGuid(); 
            var accountId = Guid.NewGuid();
            var command = new CreateBankAccountCommand { UserId = userId, Balance = 1000 };
            var bankAccountEntity = new BankAccountEntity { Id = accountId, UserId = userId, Balance = 1000 };
            var bankAccountResponse = new CreateBankAccountCommandResponse { Id = accountId, UserId = userId, Balance = 1000 };

            _mockBankAccountRepository
                .Setup(repo => repo.AddAsync(It.IsAny<BankAccountEntity>()))
                .ReturnsAsync(bankAccountEntity);

            _mockMapper
                .Setup(mapper => mapper.Map<CreateBankAccountCommandResponse>(It.IsAny<BankAccountEntity>()))
                .Returns(bankAccountResponse);

            // Act
            var result = await _handler.Handle(command);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeOfType<CreateBankAccountCommandResponse>();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(accountId);
            result.Value.UserId.Should().Be(userId);
            result.Value.Balance.Should().Be(1000);
        }

        [Fact]
        public async Task CreateBankAccountCommand_WhenAccountIsNotCreated_ReturnsBankAccountNotCreatedError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new CreateBankAccountCommand { UserId = userId, Balance = 1000 };
            var error =  Errors.BankAccount.BankAccountNotCreated;

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