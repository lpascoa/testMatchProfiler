using AutoMapper;
using TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContracts;
using TestMatchProfile.Application.Features.LegalContracts.Commands.CreateLegalContract;
using TestMatchProfile.Application.Features.Positions.Commands.CreatePosition;
using TestMatchProfile.Application.Features.Positions.Queries.GetPositions;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();


            CreateMap<LegalContract, GetLegalContractsViewModel>().ReverseMap();
            CreateMap<CreateLegalContractCommand, LegalContract>();
        }
    }
}