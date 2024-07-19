using FluentValidation;
using Inforce_Api.Models.DTO.ShortenUrl.Requests;

namespace Inforce_Api.FluentValidations.ShortenUrlValidations
{
    public class ShortenUrlRequestValidator : AbstractValidator<ShortenUrlRequest>
    {
        public ShortenUrlRequestValidator()
        {
            RuleFor(u => u.LongUrl).NotEmpty();
        }
    }
}
