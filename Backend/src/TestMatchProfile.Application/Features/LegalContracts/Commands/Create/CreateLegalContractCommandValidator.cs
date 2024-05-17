using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Interfaces.Repositories;

namespace TestMatchProfile.Application.Features.LegalContracts.Commands.CreateLegalContract
{
    public class CreateLegalContractCommandValidator : AbstractValidator<CreateLegalContractCommand>
    {
        private readonly ILegalContractRepositoryAsync _repository;

        public CreateLegalContractCommandValidator(ILegalContractRepositoryAsync repository)
        {
            _repository = repository;

            RuleFor(p => p.DescribeLegalEntity)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(4000).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}