using FluentValidation;
using Vb.Schema;

namespace Vb.Business.Validator;

public class CreateTokenValidator : AbstractValidator<TokenRequest>
{
    public CreateTokenValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(10);
    }
}