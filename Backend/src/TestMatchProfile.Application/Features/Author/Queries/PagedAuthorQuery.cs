using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.Author.Queries.GetAuthors;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Application.Wrappers;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Features.Author.Queries.GetAuthorContracts
{
    public partial class PagedAuthorQuery : IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {
        //strong type input parameters
        public int Draw { get; set; } //page number
        public int Start { get; set; } //Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        public int Length { get; set; } //page size
        public IList<SortOrder> SortOrder { get; set; } //Order by
        public Search Search { get; set; } //search criteria
        public IList<Column> Columns { get; set; } //select fields
    }

    public class PagedAuthorQueryHandler : IRequestHandler<PagedAuthorQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        private readonly IAuthorRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for PageAuthorQueryHandler class.
        /// </summary>
        /// <param name="repository">IAuthorRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// PageAuthorQueryHandler object.
        /// </returns>
        public PagedAuthorQueryHandler(IAuthorRepositoryAsync repository, IModelHelper modelHelper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the PagedAuthorsQuery request and returns a PagedDataTableResponse.
        /// </summary>
        /// <param name="request">The PagedAuthorsQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedDataTableResponse.</returns>
        public async Task<PagedDataTableResponse<IEnumerable<Entity>>> Handle(PagedAuthorQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetAuthorsQuery();

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
                //validFilter.Author = request.Search.Value;
                //validFilter.LegalEntity = request.Search.Value;
                //validFilter.CreatedProcess = request.Search.Value;
                validFilter.Describe = request.Search.Value;

            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetAuthorViewModel>();
            }
            // query based on filter
            var qryResult = await _repository.GetPagedAuthorResponseAsync(validFilter);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}