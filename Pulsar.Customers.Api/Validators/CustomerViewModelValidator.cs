using FluentValidation;
using Pulsar.Customers.Api.Data.Services;
using Pulsar.Customers.Api.Models.Customers;

namespace Pulsar.Customers.Api.Validators
{
    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 20)
                .WithMessage("Name cannot be empty");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email must be in valid email address format");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address cannot be empty");

            RuleFor(x => x.Zip)
                .Matches("^[0-9]{5}$")
               .WithMessage("Zip can only by 5 digits numbers only");

        }
    }
}