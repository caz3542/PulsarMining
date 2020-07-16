using Pulsar.CoreElements.Api.Models.HealthService;
using FluentValidation;

namespace Pulsar.CoreElements.Api.Validators
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