using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount
{
    public class WithdrawFromBankAccountRequestValidator : AbstractValidator<WithdrawFromBankAccountRequest>
    {
        public WithdrawFromBankAccountRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.AmountToWithdraw)
                .NotEmpty().WithMessage("Amount to withdraw is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Amount to withdraw has to be greater or equal than 0");
        }
    }
}
