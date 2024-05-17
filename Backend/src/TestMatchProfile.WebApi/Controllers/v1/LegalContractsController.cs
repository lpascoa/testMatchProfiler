using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMatchProfile.Application.Features.LegalContracts.Commands.CreateLegalContract;
using TestMatchProfile.Application.Features.LegalContracts.Commands.DeleteLegalContractById;
using TestMatchProfile.Application.Features.LegalContracts.Commands.UpdateLegalContract;
using TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContractById;
using TestMatchProfile.Application.Features.LegalContracts.Queries.GetLegalContracts;
using TestMatchProfile.WebApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMatchProfile.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class LegalContractsController : BaseApiController
    {

        /// <summary>
        /// Gets a list of LegalContracts based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter used to query the LegalContracts.</param>
        /// <returns>A list of LegalContracts.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetLegalContractsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// Gets a LegalContract by its Id.
        /// </summary>
        /// <param name="id">The Id of the LegalContract.</param>
        /// <returns>The LegalContract with the specified Id.</returns>
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetLegalContractByIdQuery { Id = id }));
        }

        /// <summary>
        /// Creates a new LegalContract.
        /// </summary>
        /// <param name="command">The command containing the data for the new LegalContract.</param>
        /// <returns>A 201 Created response containing the newly created LegalContract.</returns>
        [HttpPost]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CreateLegalContractCommand command)
        {
            var resp = await Mediator.Send(command);
            return CreatedAtAction(nameof(Post), resp);
        }

        /// <summary>
        /// Sends an InsertLegalContractCommand to the mediator.
        /// </summary>
        /// <param name="command">The command to be sent.</param>
        /// <returns>The result of the command.</returns>
        [HttpPost]
        [Route("Add")]
        //[Authorize]
        public async Task<IActionResult> AddMock(CreateLegalContractCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Retrieves a paged list of LegalContracts.
        /// </summary>
        /// <param name="query">The query parameters for the paged list.</param>
        /// <returns>A paged list of LegalContracts.</returns>
        [HttpPost]
        //[Authorize]
        [Route("Paged")]
        public async Task<IActionResult> Paged(PagedLegalContractQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Updates a LegalContract with the given id using the provided command.
        /// </summary>
        /// <param name="id">The id of the LegalContract to update.</param>
        /// <param name="command">The command containing the updated information.</param>
        /// <returns>The updated LegalContract.</returns>
        [HttpPut("{id}")]
        //[Authorize(Policy = AuthorizationConsts.ManagerPolicy)]
        public async Task<IActionResult> Put(int id, UpdateLegalContractCommand command)
        {
            if (id != command.IdProcess)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a LegalContract by its Id.
        /// </summary>
        /// <param name="id">The Id of the LegalContract to delete.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{id}")]
        //[Authorize(Policy = AuthorizationConsts.AdminPolicy)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteLegalContractByIdCommand { Id = id }));
        }
    }
}