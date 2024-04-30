using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyManagement.API.GrpcServices;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Features.AssessmentSurvey.Commands.CreateAssessmentSurvey;
using SurveyManagement.Application.Features.AssessmentSurvey.Commands.UpdateAssessmentSurvey;
using SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey;
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetAssessmentSurvey;
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey;
using System.Net;

namespace SurveyManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssessmentSurveyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserSurveyAssessmentRepository _usersurveyAssessmentrepository;
        private readonly RoleGrpcService _roleGrpcService;
        public AssessmentSurveyController(IMediator mediator, IUserSurveyAssessmentRepository usersurveyAssessmentrepository, RoleGrpcService roleGrpcService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _usersurveyAssessmentrepository = usersurveyAssessmentrepository ?? throw new ArgumentNullException(nameof(usersurveyAssessmentrepository));
            _roleGrpcService = roleGrpcService ?? throw new ArgumentNullException(nameof(roleGrpcService));
        }
        [HttpPost("CreateAssessmentSurvey")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateAssessmentSurvey([FromBody] CreateAssessmentSurveyCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[HttpPut("UpdateAssessmentSurvey")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult> UpdateAssessmentSurvey([FromBody] UpdateAssessmentSurveyCommand command)
        //{
        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        [HttpGet("GetAssessmentSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAssessmentSurvey()
        {
            var command = new GetAssessmentSurveyListQuery();
            var assessmentsurvey = await _mediator.Send(command);
            if (assessmentsurvey == null)
            {
                return NotFound();
            }
            return Ok(assessmentsurvey);
        }
        [HttpGet("GetAssessmentSurveyByUserSurveyAssessmentId/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAssessmentSurveyByUserSurveyAssessmentId(long id)
        {
            var command = new GetAssessmentSurveyByIdQuery(id);

            var AssessmentSurvey = await _mediator.Send(command);

            if (AssessmentSurvey == null)
            {
                return NotFound();
            }
            return Ok(AssessmentSurvey);
        }
    }
}
