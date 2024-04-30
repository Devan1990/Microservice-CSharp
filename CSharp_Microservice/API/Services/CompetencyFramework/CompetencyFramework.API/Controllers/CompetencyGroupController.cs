using ClosedXML.Excel;
using CompetencyFramework.API.Utility;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.DeleteCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.UpdateCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompetencyFramework.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    
    public class CompetencyGroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public CompetencyGroupController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
             _configuration = configuration;
          
        }

        [HttpPost("CreateCompetencyGroup")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateCompetencyGroup([FromBody] CreateCompetencyGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateCompetencyGroup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateCompetencyGroup([FromBody] UpdateCompetencyGroupCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteCompetencyGroup/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteCompetencyGroup(int id)
        {
            var command = new DeleteCompetencyGroupCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("GetCompetencyGroup/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCompetencyGroup(int id)
        {
            var command = new GetCompetencyGroupByIdQuery(id);
            var group = await _mediator.Send(command);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpGet("GetCompetencyGroups")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCompetencyGroups()
        {
            var tkn = Request.Headers["Authorization"].ToString().Replace("Bearer ","");
            var claimsdata = new[] {
                    new Claim("aud", User.Claims.First(i => i.Type == "aud").Value) };
            var command = new GetCompetencyGroupListQuery();
            var groups = await _mediator.Send(command);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }


        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var command = new GetCompetencyGroupListQuery();
            var groups = await _mediator.Send(command);
            if (groups == null)
            {
                return NotFound();
            }

            CompetencyGroupBulkUpload CG_Bulk = new CompetencyGroupBulkUpload(_mediator, groups);

            var result = await CG_Bulk.CompetencyGroupArrayAsync(file);

            return Ok(result);
        }

    }
}
