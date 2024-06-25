using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;

namespace SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount
{
    public class DepositInBankAccountCommandHandler : IDepositInBankAccountCommandHandler
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

        public async Task<ErrorOr<DepositInBankAccountCommandResponse>> Handle(DepositInBankAccountCommand command)
        {
            return new DepositInBankAccountCommandResponse();
        }
    }

    public interface IDepositInBankAccountCommandHandler
    {
        Task<ErrorOr<DepositInBankAccountCommandResponse>> Handle(DepositInBankAccountCommand command);
    }
}
