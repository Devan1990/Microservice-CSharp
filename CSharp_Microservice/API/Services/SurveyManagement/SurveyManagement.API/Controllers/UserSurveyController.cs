using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyManagement.Application.Features.UsersSurvey.Queries;
using SurveyManagement.API.GrpcServices;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Features.UsersSurvey.Commands.UpdateUserSurvey;
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey;

namespace SurveyManagement.API.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserSurveyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RoleGrpcService _roleGrpcService;
        private readonly ISurveyRepository _surveyrepository;
        private readonly IUserSurveyRepository _usersurveyrepository;
        public UserSurveyController(IMediator mediator, RoleGrpcService roleGrpcservice, ISurveyRepository surveyrepository, IUserSurveyRepository usersurveyrepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _roleGrpcService = roleGrpcservice ?? throw new ArgumentNullException(nameof(roleGrpcservice));
            _surveyrepository = surveyrepository ?? throw new ArgumentNullException(nameof(surveyrepository));
            _usersurveyrepository = usersurveyrepository ?? throw new ArgumentNullException(nameof(usersurveyrepository));
        }

        [HttpGet("GetUserSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetUserSurvey()
        {
            var command = new GetUserSurveyListQuery();
            var usersurvey = await _mediator.Send(command);
            if (usersurvey == null)
            {
                return NotFound();
            }
            var userdata = await _roleGrpcService.GetAllUsersList();
            var Roledata = await _roleGrpcService.GetRoles("");
            var RoleMappingData = await _roleGrpcService.GetRoleMappingLst(0);
            var userdatalist = userdata.Users.ToList();
            var Roledatalist = Roledata.RoleDetails.ToList();
            var RoleMappingDataList = RoleMappingData.RoleMappingData.ToList();
            var survyedata = await _surveyrepository.GetSurvey();
            var survyedataList = survyedata.ToList();
            List<UserSurveyVm2> vm = new List<UserSurveyVm2>();
            foreach (var usersurvy in usersurvey)
            {
                List<UserSurveyVm> uvm = new List<UserSurveyVm>();
                uvm.Add(usersurvy);
                foreach (var assmentdetails in usersurvy.UserSurveyAssessments)
                {
                    List<UserSurveyAssessmentVm> auvm = new List<UserSurveyAssessmentVm>();
                    auvm.Add(assmentdetails);
                    UserSurveyVm2 vm1 = new UserSurveyVm2();
                    vm1.AssessmentID = usersurvy.AssessmentID;
                    vm1.ID = assmentdetails.Id;
                    vm1.SurveyID = usersurvy.SurveyId;
                    var surveydata = survyedataList.
                                   Where(b => uvm.Any(a => a.SurveyId == b.Id)).ToList();
                    vm1.SurveyName = surveydata.Count == 0 ? "" : surveydata[0].SurveyId;
                    vm1.AssesseeUId = usersurvy.UserId;
                    var userAssesseenamedata = userdatalist.Where(b => uvm.Any(a => a.UserId == b.Id)).ToList();
                    vm1.AssesseeUserId = userAssesseenamedata[0].UserId;
                    vm1.AssesseeName = userAssesseenamedata[0].FirstName + " " + userAssesseenamedata[0].LastName;
                    vm1.AssesseeRoleName = userAssesseenamedata[0].RoleName;
                    vm1.AssesseeRoleId = userAssesseenamedata[0].RoleId;
                    vm1.Country = userAssesseenamedata[0].CountryName;
                    vm1.Area = userAssesseenamedata[0].AreaName;
                    vm1.Region = userAssesseenamedata[0].RegionName;
                    vm1.AssessorRoleId = assmentdetails.AssessorRoleId;
                    var MatchedRoles = RoleMappingDataList.
                                   Where(b => auvm.Any(a => userAssesseenamedata[0].RoleId == b.RoleId)).FirstOrDefault();

                    var assrole = MatchedRoles.AssessorRole.AssessorRole.ToList();
                    var Assessormadatorydata = assrole.
                                   Where(b => auvm.Any(a => a.AssessorRoleId == b.RoleId)).ToList();
                    vm1.AssessorStatus = Assessormadatorydata.Count == 0 ? false : Assessormadatorydata[0].Mandatory;
                    var AssessorRoledata = Roledatalist.ToList().Where(b => auvm.Any(a => a.AssessorRoleId == b.Id)).ToList();
                    vm1.AssessorRoleName = AssessorRoledata.Count == 0 ? "" : AssessorRoledata[0].RoleName;

                    var userAssessornamedata = userdatalist.Where(b => auvm.Any(a => a.AssessorId == b.Id)).ToList();
                    vm1.AssessorUserName = userAssessornamedata.Count == 0 ? "" : userAssessornamedata[0].FirstName + " " + userAssessornamedata[0].LastName;
                    vm1.AssessorUserId = userAssessornamedata.Count == 0 ? "" : userAssessornamedata[0].UserId;
                    vm1.AssessorUId = assmentdetails.AssessorId;
                    vm1.assessmentTypeId = assmentdetails.AssessmentTypeId.Id;
                    vm1.AssessmentStatus = assmentdetails.AssessmentTypeId.Assessmenttype;
                    vm.Add(vm1);
                }
            }
            return Ok(vm);
        }

        [HttpPut("UpdateUserSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateUserSurvey([FromBody] UpdateUserSurveyAssessmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPost("GetUserSurveyById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetUserSurveyById([FromBody] GetUserSurveyAssessmentByIdsQuery command)
        {
            List<AssessmentUserSurveyVm> asslst = new List<AssessmentUserSurveyVm>();
            foreach (var asid in command.UserSurvey)
            {
                var ass = new AssessmentUserSurveyVm();
                ass.Id = asid.Id;
                asslst.Add(ass);
            };
            var commandobj = new GetUserSurveyAssessmentByIdsQuery();
            commandobj.UserSurvey = asslst;
            var usersurveyAssessment = await _mediator.Send(commandobj);
            if (usersurveyAssessment == null)
            {
                return NotFound();
            }
            var ussass = usersurveyAssessment.ToList();
            List<UserSurveyVm> usersurvey = new List<UserSurveyVm>();
            for (int i = 0; i < ussass.Count; i++)
            {
                UserSurveyVm us = new UserSurveyVm();
                us.UserSurveyAssessments = ussass[i].UserSurveyId.UserSurveyAssessments;
                us.SurveyId = ussass[i].UserSurveyId.SurveyId;
                us.UserId = ussass[i].UserSurveyId.UserId;
                us.Id = ussass[i].UserSurveyId.Id;
                us.AssessmentID = ussass[i].UserSurveyId.AssessmentID;
                usersurvey.Add(us);
            }
            var userdata = await _roleGrpcService.GetAllUsersList();
            var Roledata = await _roleGrpcService.GetRoles("");
            var RoleMappingData = await _roleGrpcService.GetRoleMappingLst(0);
            var userdatalist = userdata.Users.ToList();
            var Roledatalist = Roledata.RoleDetails.ToList();
            var RoleMappingDataList = RoleMappingData.RoleMappingData.ToList();
            var survyedata = await _surveyrepository.GetSurvey();
            var survyedataList = survyedata.ToList();
            List<UserSurveyVm2> vm = new List<UserSurveyVm2>();
            foreach (var usersurvy in usersurvey)
            {
                List<UserSurveyVm> uvm = new List<UserSurveyVm>();
                uvm.Add(usersurvy);
                foreach (var assmentdetails in usersurvy.UserSurveyAssessments)
                {
                    List<UserSurveyAssessmentVm> auvm = new List<UserSurveyAssessmentVm>();
                    auvm.Add(assmentdetails);
                    UserSurveyVm2 vm1 = new UserSurveyVm2();
                    vm1.AssessmentID = usersurvy.AssessmentID;
                    vm1.ID = assmentdetails.Id;
                    vm1.SurveyID = usersurvy.SurveyId;
                    var surveydata = survyedataList.
                                   Where(b => uvm.Any(a => a.SurveyId == b.Id)).ToList();
                    vm1.SurveyName = surveydata.Count == 0 ? "" : surveydata[0].SurveyId;
                    vm1.AssesseeUId = usersurvy.UserId;
                    var userAssesseenamedata = userdatalist.Where(b => uvm.Any(a => a.UserId == b.Id)).ToList();
                    vm1.AssesseeUserId = userAssesseenamedata[0].UserId;
                    vm1.AssesseeName = userAssesseenamedata[0].FirstName + " " + userAssesseenamedata[0].LastName;
                    vm1.AssesseeRoleName = userAssesseenamedata[0].RoleName;
                    vm1.AssesseeRoleId = userAssesseenamedata[0].RoleId;
                    vm1.Country = userAssesseenamedata[0].CountryName;
                    vm1.Area = userAssesseenamedata[0].AreaName;
                    vm1.Region = userAssesseenamedata[0].RegionName;
                    vm1.AssessorRoleId = assmentdetails.AssessorRoleId;
                    var MatchedRoles = RoleMappingDataList.
                                   Where(b => auvm.Any(a => userAssesseenamedata[0].RoleId == b.RoleId)).FirstOrDefault();

                    var assrole = MatchedRoles.AssessorRole.AssessorRole.ToList();
                    var Assessormadatorydata = assrole.
                                   Where(b => auvm.Any(a => a.AssessorRoleId == b.RoleId)).ToList();
                    vm1.AssessorStatus = Assessormadatorydata.Count == 0 ? false : Assessormadatorydata[0].Mandatory;
                    var AssessorRoledata = Roledatalist.ToList().Where(b => auvm.Any(a => a.AssessorRoleId == b.Id)).ToList();
                    vm1.AssessorRoleName = AssessorRoledata.Count == 0 ? "" : AssessorRoledata[0].RoleName;

                    var userAssessornamedata = userdatalist.Where(b => auvm.Any(a => a.AssessorId == b.Id)).ToList();
                    vm1.AssessorUserName = userAssessornamedata.Count == 0 ? "" : userAssessornamedata[0].FirstName + " " + userAssessornamedata[0].LastName;
                    vm1.AssessorUserId = userAssessornamedata.Count == 0 ? "" : userAssessornamedata[0].UserId;
                    vm1.AssessorUId = assmentdetails.AssessorId;
                    vm1.assessmentTypeId = assmentdetails.AssessmentTypeId.Id;
                    vm1.AssessmentStatus = assmentdetails.AssessmentTypeId.Assessmenttype;
                    vm.Add(vm1);
                }
            }

            return Ok(vm.DistinctBy(x => x.ID).ToList());
        }


        [HttpGet("GetAssessmentTypes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAssessmentTypes()
        {
            var command = new GetAssessmentTypeListQuery();
            var assessmentype = await _mediator.Send(command);
            if (assessmentype == null)
            {
                return NotFound();
            }
            return Ok(assessmentype);
        }
    }
}
