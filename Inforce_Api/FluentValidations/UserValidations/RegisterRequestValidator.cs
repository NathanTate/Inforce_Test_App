using FluentValidation;
using Inforce_Api.Models.DTO.User.Requests;

namespace Inforce_Api.FluentValidations.UserValidations
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().Length(6, 32);
        }
    }
}
