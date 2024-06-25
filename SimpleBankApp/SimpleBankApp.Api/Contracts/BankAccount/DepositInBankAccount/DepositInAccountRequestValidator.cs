using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount
{
    public class DepositInBankAccountRequestValidator : AbstractValidator<DepositInBankAccountRequest>
    {
        public DepositInBankAccountRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.AmountToDeposit)
                .NotEmpty().WithMessage("Amount to deposit is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Balance has to be greater or equal than 0");
        }
    }
}
