using ErrorOr;
using MediatR;

namespace SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount
{
    public class CreateBankAccountCommand : IRequest<ErrorOr<CreateBankAccountCommandResponse>>
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}
