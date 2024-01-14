using FluentValidation;
using Vb.Schema;

namespace Vb.Business.Validator;

public class CreateCustomerValidator : AbstractValidator<CustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IdentityNumber).NotEmpty().MaximumLength(11).WithName("Customer tax or identity number");
        RuleFor(x => x.DateOfBirth).NotEmpty();

        RuleForEach(x => x.Addresses).SetValidator(new CreateAddressValidator());
    }
}