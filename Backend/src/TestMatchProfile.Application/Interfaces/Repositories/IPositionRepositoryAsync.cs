﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.Positions.Queries.GetPositions;
using TestMatchProfile.Application.Parameters;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for Position entity with asynchronous methods.
    /// </summary>
    /// <param name="positionNumber">Position number to check for uniqueness.</param>
    /// <returns>
    /// Task indicating whether the position number is unique.
    /// </returns>
    /// <param name="rowCount">Number of rows to seed.</param>
    /// <returns>
    /// Task indicating the completion of seeding.
    /// </returns>
    /// <param name="requestParameters">Parameters for the query.</param>
    /// <param name="data">Data to be returned.</param>
    /// <param name="recordsCount">Number of records.</param>
    /// <returns>
    /// Task containing the paged response.
    /// </returns>    
    public interface IPositionRepositoryAsync : IGenericRepositoryAsync<Position>
    {
        Task<bool> IsUniquePositionNumberAsync(string positionNumber);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedPositionReponseAsync(GetPositionsQuery requestParameters);
    }
}