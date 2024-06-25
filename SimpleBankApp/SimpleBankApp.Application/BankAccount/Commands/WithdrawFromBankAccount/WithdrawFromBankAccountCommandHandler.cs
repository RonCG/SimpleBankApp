using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;

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
            return new WithdrawFromBankAccountCommandResponse();
        }
    }

    public interface IWithdrawFromBankAccountCommandHandler
    {
        Task<ErrorOr<WithdrawFromBankAccountCommandResponse>> Handle(WithdrawFromBankAccountCommand command);
    }
}
