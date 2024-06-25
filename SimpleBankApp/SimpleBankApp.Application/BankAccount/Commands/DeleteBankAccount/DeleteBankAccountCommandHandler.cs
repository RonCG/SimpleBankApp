using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;


namespace SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommandHandler : IDeleteBankAccountCommandHandler
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public DeleteBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<DeleteBankAccountCommandResponse>> Handle(DeleteBankAccountCommand command)
        {
            var existingBankAccount = await _bankAccountRepository.GetBankAccount(command.AccountId, command.UserId);
            if (existingBankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotFound;
            }

            if (existingBankAccount.Balance > 0)
            {
                return Errors.BankAccount.CannotDeleteWithAvailableFunds;
            }

            var result = await _bankAccountRepository.DeleteAsync(existingBankAccount);
            if (!result)
            {
                return Errors.BankAccount.BankAccountNotDeleted;
            }

            return new DeleteBankAccountCommandResponse { IsDeleted = true };
        }
    }

    public interface IDeleteBankAccountCommandHandler
    {
        Task<ErrorOr<DeleteBankAccountCommandResponse>> Handle(DeleteBankAccountCommand command);
    }
}
