using ErrorOr;
using MapsterMapper;
using MediatR;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;

namespace SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount
{
    public class DepositInBankAccountCommandHandler : IRequestHandler<DepositInBankAccountCommand, ErrorOr<DepositInBankAccountCommandResponse>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public DepositInBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DepositInBankAccountCommandResponse>> Handle(DepositInBankAccountCommand command, CancellationToken cancellationToken)
        {
            var existingBankAccount = await _bankAccountRepository.GetBankAccount(command.AccountId, command.UserId);
            if(existingBankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotFound;
            }

            existingBankAccount.Balance += command.AmountToDeposit;
            var bankAccount = await _bankAccountRepository.UpdateAsync(existingBankAccount);
            if(bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotUpdated;
            }

            return _mapper.Map<DepositInBankAccountCommandResponse>(bankAccount);
        }
    }

}
