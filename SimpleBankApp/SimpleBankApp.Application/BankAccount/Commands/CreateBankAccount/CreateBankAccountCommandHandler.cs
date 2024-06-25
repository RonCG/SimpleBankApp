using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : ICreateBankAccountCommandHandler
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

        public async Task<ErrorOr<CreateBankAccountCommandResponse>> Handle(CreateBankAccountCommand command)
        {
            var bankAccountEntity = new BankAccountEntity
            {
                Balance = command.Balance,
                UserId = command.UserId
            };
            
            var bankAccount = await _bankAccountRepository.AddAsync(bankAccountEntity);
            if(bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotCreated;
            }

            return _mapper.Map<CreateBankAccountCommandResponse>(bankAccount);
        }
    }

    public interface ICreateBankAccountCommandHandler
    {
        Task<ErrorOr<CreateBankAccountCommandResponse>> Handle(CreateBankAccountCommand command);
    }
}
