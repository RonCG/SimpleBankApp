using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount
{
    public class CreateBankAccountRequestValidator : AbstractValidator<CreateBankAccountRequest>
    {
        public CreateBankAccountRequestValidator()
        {
            RuleFor(x => x.Balance)
                .NotEmpty().WithMessage("Balance is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Balance has to be greater or equal than 0");
        }
    }
}
