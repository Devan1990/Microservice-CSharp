using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Application.Features.Users.Commands.DeleteUser;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Features.Users.Commands.UpdateUser;
using UserManagement.Application.Features.Users.Queries.GetUsers;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserManagementController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        // testing purpose
        [HttpPost("CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpGet("GetUsers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetUsers()
        {
            var command = new GetUserListQuery();
            var user = await _mediator.Send(command);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("GetUser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetUser(int id)
        {
            var command = new GetUserByIdQuery(id);
            var user = await _mediator.Send(command);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("GetCountries")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCountries()
        {
            var command = new GetCountryListQuery();
            var country = await _mediator.Send(command);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpGet("GetAreas")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAreas()
        {
            var command = new GetAreaListQuery();
            var area = await _mediator.Send(command);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }
        [HttpGet("GetRegions")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRegions()
        {
            var command = new GetRegionListQuery();
            var area = await _mediator.Send(command);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }

        [HttpGet("GetVerticals")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetVerticals()
        {
            var command = new GetVerticalsListQuery();
            var Verticals = await _mediator.Send(command);
            if (Verticals == null)
            {
                return NotFound();
            }
            return Ok(Verticals);
        }
    }
}
