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

namespace TestMatchProfile.Application.Features.Author.Queries.GetAuthors
{
    /// <summary>
    /// GetAllAuthorsQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetAuthorsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public int IdAuthor { get; set; }

        public string Describe { get; set; }
        public DateTime? CreatedProcess { get; set; }
        public DateTime? UpdatedProcess { get; set; }

    }

    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IAuthorRepositoryAsync _authorRepository;
        private readonly IModelHelper _modelHelper;



        /// <summary>
        /// Constructor for GetAllAuthorsQueryHandler class.
        /// </summary>
        /// <param name="authorRepository">IEmployeeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllAuthorsQueryHandler object.
        /// </returns>
        public GetAllAuthorsQueryHandler(IAuthorRepositoryAsync authorRepository, IModelHelper modelHelper)
        {
            _authorRepository = authorRepository;
            _modelHelper = modelHelper;
        }



        /// <summary>
        /// Handles the GetAuthorsQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetAuthorsQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetAuthorViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetAuthorViewModel>();
            }
            // query based on filter
            var qryResult = await _authorRepository.GetPagedAuthorResponseAsync(validFilter);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}