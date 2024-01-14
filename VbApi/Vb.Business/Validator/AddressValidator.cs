using FluentValidation;
using Vb.Schema;

namespace Vb.Business.Validator;

public class CreateAddressValidator : AbstractValidator<AddressRequest>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(6).MinimumLength(6).WithName("Zip code or postal code");
        RuleFor(x => x.Address1).NotEmpty().MinimumLength(20).MaximumLength(100).WithName("Customer address line 1");
        RuleFor(x => x.Address2).NotEmpty().MaximumLength(100).WithName("Customer address line 2");
    }
}