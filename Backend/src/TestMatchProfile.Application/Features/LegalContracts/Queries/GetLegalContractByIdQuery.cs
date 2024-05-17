using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Exceptions;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Wrappers;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContractById
{
    public class GetLegalContractByIdQuery : IRequest<Response<LegalContract>>
    {
        public Guid Id { get; set; }

        public class GetLegalContractByIdQueryHandler : IRequestHandler<GetLegalContractByIdQuery, Response<LegalContract>>
        {
            private readonly ILegalContractRepositoryAsync _repository;

            public GetLegalContractByIdQueryHandler(ILegalContractRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<LegalContract>> Handle(GetLegalContractByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"LegalContract Not Found.");
                return new Response<LegalContract>(entity);
            }
        }
    }
}