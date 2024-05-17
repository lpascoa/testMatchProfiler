using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContracts;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Application.Wrappers;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContracts
{
    /// <summary>
    /// GetAllLegalContractsQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetLegalContractsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public int IdProcess { get; set; }
        public int Author { get; set; }
        public int LegalEntity { get; set; }
        public string AuthorName { get; set; }
        public string LegalEntityName { get; set; }
        public string DescribeLegalEntity { get; set; }
        public DateTime? CreatedProcess { get; set; }
        public DateTime? UpdatedProcess { get; set; }

    }

    public class GetAllLegalContractsQueryHandler : IRequestHandler<GetLegalContractsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ILegalContractRepositoryAsync _legalContractRepository;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for GetAllLegalContractsQueryHandler class.
        /// </summary>
        /// <param name="legalContractRepository">IEmployeeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllLegalContractsQueryHandler object.
        /// </returns>
        public GetAllLegalContractsQueryHandler(ILegalContractRepositoryAsync legalContractRepository, IModelHelper modelHelper)
        {
            _legalContractRepository = legalContractRepository;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the GetLegalContractsQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetLegalContractsQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetLegalContractsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetLegalContractsViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetLegalContractsViewModel>();
            }
            // query based on filter
            var qryResult = await _legalContractRepository.GetPagedLegalContractResponseAsync(validFilter);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}