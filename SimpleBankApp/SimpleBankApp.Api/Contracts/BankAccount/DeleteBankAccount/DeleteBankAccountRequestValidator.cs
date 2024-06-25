using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.DeleteBankAccount
{
    public class DeleteBankAccountRequestValidator : AbstractValidator<DeleteBankAccountRequest>
    {
        public DeleteBankAccountRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}
