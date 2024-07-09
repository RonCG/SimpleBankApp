using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountQueryHandler : IGetBankAccountQueryHandler
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountQueryHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetBankAccountQueryResponse>> Handle(GetBankAccountQuery command)
        {
            var bankAccount = await _bankAccountRepository.GetBankAccount(command.AccountId, command.UserId);
            if (bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotFound;
            }

            return _mapper.Map<GetBankAccountQueryResponse>(bankAccount);
        }
    }

    public interface IGetBankAccountQueryHandler
    {
        Task<ErrorOr<GetBankAccountQueryResponse>> Handle(GetBankAccountQuery command);
    }
}
