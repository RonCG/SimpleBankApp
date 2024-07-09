using ErrorOr;
using MediatR;

namespace SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount
{
    public class WithdrawFromBankAccountCommand : IRequest<ErrorOr<WithdrawFromBankAccountCommandResponse>>
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public decimal AmountToWithdraw { get; set; } = 0;
    }
}
