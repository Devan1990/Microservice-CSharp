using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyManagement.API.GrpcServices;
using SurveyManagement.Application.Features.Survey.Commands.CreateSurvey;
using SurveyManagement.Application.Features.Survey.Commands.DeleteSurvey;
using SurveyManagement.Application.Features.Survey.Commands.UpdateSurvey;
using SurveyManagement.Application.Features.Survey.Queries.GetSurvey;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SurveyManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SurveyManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RoleGrpcService _roleGrpcService;

        public SurveyManagementController(IMediator mediator, RoleGrpcService roleGrpcService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _roleGrpcService = roleGrpcService ?? throw new ArgumentNullException(nameof(roleGrpcService));

        }

        // testing purpose
        [HttpPost("CreateSurvey")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateSurvey([FromBody] CreateSurveyCommand command)
        {
            var roleExists = await _roleGrpcService.IsRoleExistsValue(command.RoleId);

            if (!roleExists.Exists)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //[HttpGet("GetSurveyRoles")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult> GetSurveyRoles()
        //{
        //    var roleExists = await _roleGrpcService.GetRoles("Test");



        //    return Ok(roleExists);
        //}


        [HttpGet("GetSurveys")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetSurvey()
        {
            var command = new GetSurveyListQuery();
            var survey = await _mediator.Send(command);
            if (survey == null)
            {
                return NotFound();
            }
            var rolelst = await _roleGrpcService.GetRoles("Test");
            var rl = rolelst.RoleDetails.ToList();
            for (int i = 0; i < survey.Count; i++)
             {
               
                  for (int r = 0; r < rl.Count; r++)
                    {
                        if (survey[i].RoleId == rl[r].Id)
                        {
                         survey[i].RoleName = rl[r].RoleName;
                            survey[i].RoleAID = rl[r].RoleId.ToString();
                       }
                   }
                   
                 
               
              }
            
            return Ok(survey);
        }
        [HttpGet("GetSurvey/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetSurvey(long id)
        {
            var command = new GetSurveyByIdQuery(id);

            var surveys = await _mediator.Send(command);

            if (surveys == null)
            {
                return NotFound();
            }

            List<SurveyVm> surveylst = new List<SurveyVm>();
            surveylst.Add(surveys);

            var rolelst = await _roleGrpcService.GetRoles("Test");
            var rl = rolelst.RoleDetails.ToList();
            for (int i = 0; i < surveylst.Count; i++)
            {
                //for (int j = 0; j < surveylst[i].SurveyRoleMappings.Count; j++)
                //{
                    for (int r = 0; r < rl.Count; r++)
                    {
                        if (surveylst[i].RoleId == rl[r].Id)
                        {
                            surveylst[i].RoleName = rl[r].RoleName;
                            surveylst[i].RoleAID = rl[r].RoleId.ToString();
                        }
                    }



                
            }
            return Ok(surveys);
        }
        [HttpPut("UpdateSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateSurvey([FromBody] UpdateSurveyCommand command)
        {
            var roleExists = await _roleGrpcService.IsRoleExistsValue(command.RoleId);

            if (!roleExists.Exists)
            {
                return BadRequest();
            }


            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteSurvey(int id)
        {
            var command = new DeleteSurveyCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        
    }
}

