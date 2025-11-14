using CleanArchitecture.Application.Commands.Company;
using FluentValidation;
using System.Data;

namespace CleanArchitecture.Application.Validations.Company
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("The company's name cannot be empty.");

            RuleFor(command => command.RegisterNumber)
                .NotEmpty()
                .WithMessage("The company's register number cannot be empty.")
                .Length(14)
                .WithMessage("The register number must contain 14 characters");
        }
    }
}
