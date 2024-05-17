using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.LegalEntity.Queries.GetLegalEntity;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Application.Wrappers;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Features.LegalEntity.Queries.GetLegalEntityContracts
{
    public partial class PagedLegalEntityQuery : IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {
        //strong type input parameters
        public int Draw { get; set; } //page number
        public int Start { get; set; } //Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        public int Length { get; set; } //page size
        public IList<SortOrder> SortOrder { get; set; } //Order by
        public Search Search { get; set; } //search criteria
        public IList<Column> Columns { get; set; } //select fields
    }

    public class PagedLegalEntityQueryHandler : IRequestHandler<PagedLegalEntityQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        private readonly ILegalEntityRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for PageLegalEntityQueryHandler class.
        /// </summary>
        /// <param name="repository">ILegalEntityRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// PageLegalEntityQueryHandler object.
        /// </returns>
        public PagedLegalEntityQueryHandler(ILegalEntityRepositoryAsync repository, IModelHelper modelHelper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the PagedLegalEntitysQuery request and returns a PagedDataTableResponse.
        /// </summary>
        /// <param name="request">The PagedLegalEntitysQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedDataTableResponse.</returns>
        public async Task<PagedDataTableResponse<IEnumerable<Entity>>> Handle(PagedLegalEntityQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetLegalEntityQuery();

            // Draw map to PageNumber
            validFilter.PageNumber = (request.Start / request.Length) + 1;
            // Length map to PageSize
            validFilter.PageSize = request.Length;

            // Map order > OrderBy
            var colOrder = request.SortOrder[0];
            switch (colOrder.Column)
            {
                case 0:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "DescribeLegalEntity" : "DescribeLegalEntity DESC";
                    break;
            }

            // Map Search > searchable columns
            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                //limit to fields in view model
                //validFilter.LegalEntity = request.Search.Value;
                //validFilter.LegalEntity = request.Search.Value;
                //validFilter.CreatedProcess = request.Search.Value;
                validFilter.Describe = request.Search.Value;

            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetLegalEntityViewModel>();
            }
            // query based on filter
            var qryResult = await _repository.GetPagedLegalEntityResponseAsync(validFilter);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}