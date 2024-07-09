using ErrorOr;
using MediatR;

namespace SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommand : IRequest<ErrorOr<DeleteBankAccountCommandResponse>>
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
    }
}
