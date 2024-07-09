using ErrorOr;
using MapsterMapper;
using MediatR;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, ErrorOr<CreateBankAccountCommandResponse>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public CreateBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<CreateBankAccountCommandResponse>> Handle(CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var bankAccountEntity = new BankAccountEntity
            {
                Balance = command.Balance,
                UserId = command.UserId
            };

            var bankAccount = await _bankAccountRepository.AddAsync(bankAccountEntity);
            if (bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotCreated;
            }

            return _mapper.Map<CreateBankAccountCommandResponse>(bankAccount);
        }
    }
}
