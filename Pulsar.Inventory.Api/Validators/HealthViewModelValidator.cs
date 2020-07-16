using Pulsar.Inventory.Api.Models.HealthService;
using FluentValidation;

namespace Pulsar.Inventory.Api.Validators
{
    public class HealthViewModelValidator : AbstractValidator<HealthServiceDetailReadViewModel>
    {
        public HealthViewModelValidator()
        {
            //RuleFor(x => x.Health)
            //    .NotEmpty();
        }
    }
}