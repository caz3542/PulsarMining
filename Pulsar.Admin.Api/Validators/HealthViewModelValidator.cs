using Pulsar.Admin.Api.Models.HealthService;
using FluentValidation;

namespace Pulsar.Admin.Api.Validators
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