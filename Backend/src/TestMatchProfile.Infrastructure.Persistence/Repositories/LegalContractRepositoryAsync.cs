using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContracts;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Application.Interfaces.Repositories;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Domain.Entities;
using TestMatchProfile.Infrastructure.Persistence.Contexts;
using TestMatchProfile.Infrastructure.Persistence.Repository;

namespace TestMatchProfile.Infrastructure.Persistence.Repositories
{
    public class LegalContractRepositoryAsync : GenericRepositoryAsync<LegalContract>, ILegalContractRepositoryAsync
    {
        private readonly IDataShapeHelper<LegalContract> _dataShaper;
        private readonly DbSet<LegalContract> _repository;

        /// <summary>
        /// Constructor for LegalContractRepositoryAsync class.
        /// </summary>
        /// <param name="dataShaper">IDataShapeHelper object.</param>
        /// <param name="dbContext">ApplicationDbContext object.</param>
        /// <returns>
        /// 
        /// </returns>
        public LegalContractRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<LegalContract> dataShaper) : base(dbContext)
        {
            _dataShaper = dataShaper;
            _repository = dbContext.Set<LegalContract>();
        }

        /// <summary>
        /// Retrieves a paged list of LegalContract based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">The query parameters used to filter and page the data.</param>
        /// <returns>A tuple containing the paged list of LegalContract and the total number of records.</returns>
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedLegalContractResponseAsync(GetLegalContractsQuery requestParameters)
        {
            var authorName = requestParameters.AuthorName ;
            var legalEntityName = requestParameters.LegalEntityName ;

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
            FilterByColumn(ref result, authorName, legalEntityName);

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
                result = result.Select<LegalContract>("new(" + fields + ")");
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
        /// Filters an IQueryable of LegalContract based on the provided parameters.
        /// </summary>
        /// <param name="qry">The IQueryable of LegalContract to filter.</param>
        /// <param name="authorName">The employee title to filter by.</param>
        /// <param name="legalEntityName">The last name to filter by.</param>
        private void FilterByColumn(ref IQueryable<LegalContract> qry, string authorName, string legalEntityName)
        {
            if (!qry.Any())
                return;

            if (string.IsNullOrEmpty(authorName) && string.IsNullOrEmpty(legalEntityName))
                return;

            var predicate = PredicateBuilder.New<LegalContract>();

            if (!string.IsNullOrEmpty(authorName))
                predicate = predicate.Or(p => p.AuthorNavigation.Describe.ToLower().Contains(authorName.ToLower().Trim()));

            if (!string.IsNullOrEmpty(legalEntityName))
                predicate = predicate.Or(p => p.LegalEntityNavigation.Describe.ToLower().Contains(authorName.ToLower().Trim()));

            qry = qry.Where(predicate);
        }
    }
}