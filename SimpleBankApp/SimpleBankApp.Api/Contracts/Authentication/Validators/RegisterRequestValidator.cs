using FluentValidation;
using SimpleBankApp.Api.Contracts.Authentication.Request;


namespace SimpleBankApp.Api.Contracts.Authentication.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
