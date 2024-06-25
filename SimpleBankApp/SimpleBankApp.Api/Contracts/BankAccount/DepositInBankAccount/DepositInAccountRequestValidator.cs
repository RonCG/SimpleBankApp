using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount
{
    public class DepositInBankAccountRequestValidator : AbstractValidator<DepositInBankAccountRequest>
    {
        public DepositInBankAccountRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.AmountToDeposit)
                .GreaterThan(0).WithMessage("Amount to deposit has to be greater than 0");
        }
    }
}
