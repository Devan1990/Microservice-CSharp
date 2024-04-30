using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Application.Features.Role.Commands.CreateRole;
using UserManagement.Application.Features.Role.Commands.DeleteRole;
using UserManagement.Application.Features.Role.Commands.UpdateRole;
using UserManagement.Application.Features.Role.Queries.GetRoles;
using UserManagement.Application.Features.Role.Queries.GetUsers;

namespace UserManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleManagementController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // testing purpose
        [HttpPost("CreateRole")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet("GetRoles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRoles()
        {
            var command = new GetRoleListQuery();
            var roles = await _mediator.Send(command);
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }
        [HttpGet("GetRole/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRole(long id)
        {
            var command = new GetRoleByIdQuery(id);
            var roles = await _mediator.Send(command);
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }


        [HttpPut("UpdateRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("DeleteRole/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var command = new DeleteRoleCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("GetRoleTypes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRoleTypes()
        {
            var command = new GetRoleTypeListQuery();
            var roletypes = await _mediator.Send(command);
            if (roletypes == null)
            {
                return NotFound();
            }
            return Ok(roletypes);
        }
        [HttpGet("GetExpectedLevels")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetExpectedLevels()
        {
            var command = new GetExpectedLevelListQuery();
            var roles = await _mediator.Send(command);
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }
    }
}
