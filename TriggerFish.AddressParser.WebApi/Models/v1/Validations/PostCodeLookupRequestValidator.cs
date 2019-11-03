using FluentValidation;

namespace TriggerFish.AddressParser.WebApi.Models.v1.Validations
{
    //TODO: unit
    public class PostCodeLookupRequestValidator : AbstractValidator<PostCodeLookupRequest>
    {
        public PostCodeLookupRequestValidator()
        {
            RuleFor(r => r.Addresses)
                .NotEmpty();

            RuleForEach(r => r.Addresses)
                .Must(a => !string.IsNullOrWhiteSpace(a))
                .WithMessage("Address cannot be null, empty or white space.");
        }
    }
}