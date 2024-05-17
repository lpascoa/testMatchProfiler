using System;
using TestMatchProfile.Domain.Enums;

namespace TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContracts
{
    public class GetLegalContractsViewModel
    {
        public int IdProcess { get; set; }
        public int Author { get; set; }
        public int LegalEntity { get; set; }
        public string DescribeLegalEntity { get; set; }
        public int? CreatedProcess { get; set; }
        public int? UpdatedProcess { get; set; }
    }
}