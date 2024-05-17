using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Wrappers;

namespace TestMatchProfile.Application.Features.LegalContracts.Commands.CreateLegalContract
{
    public partial class InsertMockLegalContractCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedLegalContractCommandHandler : IRequestHandler<InsertMockLegalContractCommand, Response<int>>
    {
        private readonly ILegalContractRepositoryAsync _repository;

        public SeedLegalContractCommandHandler(ILegalContractRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(InsertMockLegalContractCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new Domain.Entities.LegalContract());
            return new Response<int>(request.RowCount);
        }
    }
}