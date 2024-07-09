using ErrorOr;
using MapsterMapper;
using MediatR;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountQueryHandler : IRequestHandler<GetBankAccountQuery, ErrorOr<GetBankAccountQueryResponse>>
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

        public async Task<ErrorOr<GetBankAccountQueryResponse>> Handle(GetBankAccountQuery command, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankAccountRepository.GetBankAccount(command.AccountId, command.UserId);
            if (bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotFound;
            }

            return _mapper.Map<GetBankAccountQueryResponse>(bankAccount);
        }
    }

}
