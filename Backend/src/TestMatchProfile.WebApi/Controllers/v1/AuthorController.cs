using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TestMatchProfile.Application.Features.Author.Queries.GetAuthors;

namespace TestMatchProfile.WebApi.Controllers.v1
{
    public class AuthorController : BaseApiController
    {

        /// <summary>
        /// Gets a list of Authors based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter used to query the Authors.</param>
        /// <returns>A list of Authors.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAuthorsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }
    }
}
