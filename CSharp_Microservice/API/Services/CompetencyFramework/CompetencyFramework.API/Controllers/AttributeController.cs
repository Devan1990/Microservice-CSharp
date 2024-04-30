using CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute;
using CompetencyFramework.Application.Features.Attribute.Commands.DeleteAttribute;
using CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute;
using CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute;
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
    public class AttributeController : Controller
    {
        private readonly IMediator _mediator;

        public AttributeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("CreateAttribute")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateAttribute([FromBody] CreateAttributeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateAttribute")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateAttributeGroup([FromBody] UpdateAttributeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteAttribute/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteAttribute(int id)
        {
            var command = new DeleteAttributeCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("GetAttribute/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAttribute(int id)
        {
            var command = new GetAttributeByIdQuery(id);
            var attribute = await _mediator.Send(command);
            if (attribute == null)
            {
                return NotFound();
            }
            return Ok(attribute);
        }

        [HttpGet("GetAttributes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAttributes()
        {
            var command = new GetAttributeListQuery();
            var groups = await _mediator.Send(command);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }
    }
}
