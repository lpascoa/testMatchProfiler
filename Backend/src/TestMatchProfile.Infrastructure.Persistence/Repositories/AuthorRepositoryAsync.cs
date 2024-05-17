using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.Author.Queries.GetAuthors;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Domain.Entities;
using TestMatchProfile.Infrastructure.Persistence.Contexts;
using TestMatchProfile.Infrastructure.Persistence.Repository;

namespace TestMatchProfile.Infrastructure.Persistence.Repositories
{
    public class AuthorRepositoryAsync : GenericRepositoryAsync<Author>, IAuthorRepositoryAsync
    {
        private readonly IDataShapeHelper<Author> _dataShaper;
        private readonly DbSet<Author> _repository;

        /// <summary>
        /// Constructor for AuthorRepositoryAsync class.
        /// </summary>
        /// <param name="dataShaper">IDataShapeHelper object.</param>
        /// <param name="dbContext">ApplicationDbContext object.</param>
        /// <returns>
        /// 
        /// </returns>
        public AuthorRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Author> dataShaper) : base(dbContext)
        {
            _dataShaper = dataShaper;
            _repository = dbContext.Set<Author>();
        }

        /// <summary>
        /// Retrieves a paged list of Author based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">The query parameters used to filter and page the data.</param>
        /// <returns>A tuple containing the paged list of Author and the total number of records.</returns>
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedAuthorResponseAsync(GetAuthorsQuery requestParameters)
        {
            var authorName = requestParameters.Describe ;

            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _repository
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = result.Count();

            // filter data
            FilterByColumn(ref result, authorName);

            // Count records after filter
            recordsFiltered = result.Count();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            //limit query fields
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Author>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            // retrieve data to list
            // var resultData = await result.ToListAsync();
            // Note: Bogus library does not support await for AsQueryable.
            // Workaround:  fake await with Task.Run and use regular ToList
            var resultData = await Task.Run(() => result.ToList());

            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        /// <summary>
        /// Filters an IQueryable of Author based on the provided parameters.
        /// </summary>
        /// <param name="qry">The IQueryable of Author to filter.</param>
        /// <param name="authorName">The employee title to filter by.</param>
        /// <param name="authorName">The last name to filter by.</param>
        private void FilterByColumn(ref IQueryable<Author> qry, string authorName)
        {
            if (!qry.Any())
                return;

            if (string.IsNullOrEmpty(authorName) && string.IsNullOrEmpty(authorName))
                return;

            var predicate = PredicateBuilder.New<Author>();

            if (!string.IsNullOrEmpty(authorName))
                predicate = predicate.Or(p => p.Describe.ToLower().Contains(authorName.ToLower().Trim()));

            qry = qry.Where(predicate);
        }
    }
}