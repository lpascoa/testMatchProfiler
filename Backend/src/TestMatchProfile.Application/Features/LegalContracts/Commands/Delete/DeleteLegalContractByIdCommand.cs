using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Exceptions;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Wrappers;

namespace TestMatchProfile.Application.Features.LegalContracts.Commands.DeleteLegalContractById
{
    public class DeleteLegalContractByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeleteLegalContractByIdCommandHandler : IRequestHandler<DeleteLegalContractByIdCommand, Response<int>>
        {
            private readonly ILegalContractRepositoryAsync _repository;

            public DeleteLegalContractByIdCommandHandler(ILegalContractRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<int>> Handle(DeleteLegalContractByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(command.Id);
                if (entity == null) throw new ApiException($"LegalContract Not Found.");
                await _repository.DeleteAsync(entity);
                return new Response<int>(entity.IdProcess);
            }
        }
    }
}