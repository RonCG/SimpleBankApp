using FluentValidation;

namespace SimpleBankApp.Api.Contracts.BankAccount.GetBankAccount
{
    public class GetBankAccountRequestValidator : AbstractValidator<GetBankAccountRequest>
    {
        public GetBankAccountRequestValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty();
              
        }
    }
}
