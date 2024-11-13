using Configuration.Application.Configurations.Commands;
using FluentValidation;

namespace Configuration.Application.Configurations.Validators
{
    public class UpdateConfigurationCommandValidator : AbstractValidator<UpdateConfigurationCommand>
    {
        public UpdateConfigurationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name field cannot be empty.")
                .Length(3, 100).WithMessage("The Name field must be between 3 and 100 characters.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("The Type field cannot be empty.");

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("The Value field cannot be empty.")
                .MaximumLength(500).WithMessage("The Value field can be a maximum of 500 characters.");

            RuleFor(x => x.ApplicationName)
                .NotEmpty().WithMessage("The ApplicationName field cannot be empty.")
                .Length(2, 50).WithMessage("The ApplicationName field must be between 2 and 50 characters.");
        }
    }
}