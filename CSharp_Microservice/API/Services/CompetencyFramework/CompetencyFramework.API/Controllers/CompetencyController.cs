using CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency;
using CompetencyFramework.Application.Features.Competency.Commands.DeleteCompetency;
using CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency;
using CompetencyFramework.Application.Features.Competency.Queries.GetCompetency;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CompetencyFramework.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompetencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompetencyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // testing purpose
        [HttpPost("CreateCompetency")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateCompetency([FromBody] CreateCompetencyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateCompetency")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCompetencyGroup([FromBody] UpdateCompetencyCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteCompetency/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCompetency(int id)
        {
            var command = new DeleteCompetencyCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("GetCompetency/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCompetency(int id)
        {
            var command = new GetCompetencyByIdQuery(id);
            var competency = await _mediator.Send(command);
            if (competency == null)
            {
                return NotFound();
            }
            return Ok(competency);
        }

        [HttpGet("GetCompetencies")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCompetencies()
        {
            var command = new GetCompetencyListQuery();
            var competencies = await _mediator.Send(command);
            if (competencies == null)
            {
                return NotFound();
            }
            return Ok(competencies);
        }
    }
}
