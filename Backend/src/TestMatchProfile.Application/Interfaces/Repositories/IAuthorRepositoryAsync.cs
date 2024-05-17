using System.Collections.Generic;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.Author.Queries.GetAuthors;
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
    public interface IAuthorRepositoryAsync : IGenericRepositoryAsync<Author>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedAuthorResponseAsync(GetAuthorsQuery requestParameters);
    }
}