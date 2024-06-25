using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount
{
    public class CreateBankAccountRequestValidator : AbstractValidator<CreateBankAccountRequest>
    {
        public CreateBankAccountRequestValidator()
        {
            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance has to be greater or equal to 0");
        }
    }
}
