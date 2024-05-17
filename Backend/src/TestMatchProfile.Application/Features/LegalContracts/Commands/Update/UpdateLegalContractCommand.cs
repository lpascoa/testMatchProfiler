using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Exceptions;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Wrappers;

namespace TestMatchProfile.Application.Features.LegalContracts.Commands.UpdateLegalContract
{
    public class UpdateLegalContractCommand : IRequest<Response<int>>
    {

        public int IdProcess { get; set; }
        public int IdAuthor { get; set; }
        public int IdLegalEntity { get; set; }
        public string DescribeLegalEntity { get; set; }

        public class UpdateLegalContractCommandHandler : IRequestHandler<UpdateLegalContractCommand, Response<int>>
        {
            private readonly ILegalContractRepositoryAsync _repository;

            public UpdateLegalContractCommandHandler(ILegalContractRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateLegalContractCommand command, CancellationToken cancellationToken)
            {
                var LegalContract = await _repository.GetByIdAsync(command.IdProcess);

                if (LegalContract == null)
                {
                    throw new ApiException($"LegalContract Not Found.");
                }
                else
                {
                    LegalContract.Author  = command.IdAuthor;
                    LegalContract.LegalEntity = command.IdLegalEntity;
                    LegalContract.DescribeLegalEntity = command.DescribeLegalEntity;
                    LegalContract.UpdatedProcess = DateTime.Now;
                    await _repository.UpdateAsync(LegalContract);
                    return new Response<int>(LegalContract.IdProcess);
                }
            }
        }
    }
}