using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountCommandHandler : IGetBankAccountCommandHandler
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetBankAccountCommandResponse>> Handle(GetBankAccountCommand command)
        {
            return new GetBankAccountCommandResponse();
        }
    }

    public interface IGetBankAccountCommandHandler
    {
        Task<ErrorOr<GetBankAccountCommandResponse>> Handle(GetBankAccountCommand command);
    }
}
