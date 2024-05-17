using FluentValidation;
using TestMatchProfile.Application.Features.LegalContracts.Commands.UpdateLegalContract;

using TestMatchProfile.Application.Interfaces.Repositories;

namespace TestMatchProfile.Application.Features.LegalContracts.Commands.CreateLegalContracts
{
    public class UpdateLegalContractCommandValidator : AbstractValidator<UpdateLegalContractCommand>
    {
        public UpdateLegalContractCommandValidator(ILegalContractRepositoryAsync repository)
        {
            RuleFor(p => p.DescribeLegalEntity)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(4000).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}