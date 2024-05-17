using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Application.Wrappers;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Features.LegalEntity.Queries.GetLegalEntity
{
    /// <summary>
    /// GetAllLegalEntityQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetLegalEntityQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public int IdLegalEntity { get; set; }
        public string Describe { get; set; }
        public DateTime? CreatedProcess { get; set; }
        public DateTime? UpdatedProcess { get; set; }

    }

    public class GetAllLegalEntityQueryHandler : IRequestHandler<GetLegalEntityQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly ILegalEntityRepositoryAsync _LegalEntityRepository;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for GetAllLegalEntityQueryHandler class.
        /// </summary>
        /// <param name="legalEntityRepository">IEmployeeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllLegalEntityQueryHandler object.
        /// </returns>
        public GetAllLegalEntityQueryHandler(ILegalEntityRepositoryAsync legalEntityRepository, IModelHelper modelHelper)
        {
            _LegalEntityRepository = legalEntityRepository;
            _modelHelper = modelHelper;
        }

        /// <summary>
        /// Handles the GetLegalEntityQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetLegalEntityQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetLegalEntityQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetLegalEntityViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetLegalEntityViewModel>();
            }
            // query based on filter
            var qryResult = await _LegalEntityRepository.GetPagedLegalEntityResponseAsync(validFilter);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}