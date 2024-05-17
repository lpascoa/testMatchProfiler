using System.Collections.Generic;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.LegalEntity.Queries.GetLegalEntity;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged employee response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface ILegalEntityRepositoryAsync : IGenericRepositoryAsync<LegalEntity>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedLegalEntityResponseAsync(GetLegalEntityQuery requestParameters);
    }
}