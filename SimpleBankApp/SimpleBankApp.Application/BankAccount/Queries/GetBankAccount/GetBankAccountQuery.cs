using ErrorOr;
using MediatR;

namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountQuery : IRequest<ErrorOr<GetBankAccountQueryResponse>>
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
    }
}
