using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Wrappers;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Features.LegalContracts.Commands.CreateLegalContract
{
    public partial class CreateLegalContractCommand : IRequest<Response<int>>
    {
        public int IdAuthor { get; set; }
        public int IdLegalEntity { get; set; }
        public string DescribeLegalEntity { get; set; }
    }

    public class CreateLegalContractCommandHandler : IRequestHandler<CreateLegalContractCommand, Response<int>>
    {
        private readonly ILegalContractRepositoryAsync _repository;
        private readonly IMapper _mapper;

        public CreateLegalContractCommandHandler(ILegalContractRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLegalContractCommand request, CancellationToken cancellationToken)
        {
            var LegalContract = _mapper.Map<LegalContract>(request);
            LegalContract.CreatedProcess = DateTime.Now;
            await _repository.AddAsync(LegalContract);
            return new Response<int>(LegalContract.IdProcess);
        }
    }
}