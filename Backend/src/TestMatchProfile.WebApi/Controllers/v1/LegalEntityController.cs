using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TestMatchProfile.Application.Features.LegalEntity.Queries.GetLegalEntity;

namespace TestMatchProfile.WebApi.Controllers.v1
{
    public class LegalEntityController : BaseApiController
    {

        /// <summary>
        /// Gets a list of LegalEntitys based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter used to query the LegalEntitys.</param>
        /// <returns>A list of LegalEntitys.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetLegalEntityQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }
    }
}
