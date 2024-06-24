using FluentValidation;
using SimpleBankApp.Api.Contracts.Authentication.Request;


namespace SimpleBankApp.Api.Contracts.Authentication.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
