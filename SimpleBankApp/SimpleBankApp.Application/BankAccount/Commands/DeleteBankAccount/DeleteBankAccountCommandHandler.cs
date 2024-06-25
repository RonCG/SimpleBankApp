using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
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
            return new DeleteBankAccountCommandResponse();
        }
    }

    public interface IDeleteBankAccountCommandHandler
    {
        Task<ErrorOr<DeleteBankAccountCommandResponse>> Handle(DeleteBankAccountCommand command);
    }
}
