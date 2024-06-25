using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;


namespace SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount
{
    public class WithdrawFromBankAccountCommandHandler : IWithdrawFromBankAccountCommandHandler
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public WithdrawFromBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<WithdrawFromBankAccountCommandResponse>> Handle(WithdrawFromBankAccountCommand command)
        {
            var existingBankAccount = await _bankAccountRepository.GetBankAccount(command.AccountId, command.UserId);
            if (existingBankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotFound;
            }

            if(existingBankAccount.Balance < command.AmountToWithdraw)
            {
                return Errors.BankAccount.InsufficientFundsForWithdraw;
            }

            existingBankAccount.Balance -= command.AmountToWithdraw;
            var bankAccount = await _bankAccountRepository.UpdateAsync(existingBankAccount);
            if (bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotUpdated;
            }

            return _mapper.Map<WithdrawFromBankAccountCommandResponse>(bankAccount);
        }
    }

    public interface IWithdrawFromBankAccountCommandHandler
    {
        Task<ErrorOr<WithdrawFromBankAccountCommandResponse>> Handle(WithdrawFromBankAccountCommand command);
    }
}
