using CAT.BFF.Models;
using CAT.BFF.Services;
using CATBFF.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace CAT.BFF.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AssessmentUserSurveyController : ControllerBase
    {
        private readonly ICompetencyFrameworkService _competencyFrameworkService;
        private readonly IUserManagementService _userManagementService;
        private readonly ISurveyManagementService _surveyManagementService;
        public AssessmentUserSurveyController(ICompetencyFrameworkService competencyFrameworkService, IUserManagementService userManagementService, ISurveyManagementService surveyManagementService)
        {
            _competencyFrameworkService = competencyFrameworkService ?? throw new ArgumentNullException(nameof(competencyFrameworkService));
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
            _surveyManagementService = surveyManagementService ?? throw new ArgumentNullException(nameof(surveyManagementService));
        }
        [HttpPost("GetAssessmentSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAssessmentSurvey(GetUserSurveyAssessmentByIdsQuery command)
        {
            var tkn = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //var UserSurvey = await _surveyManagementService.GetUserSurvey();
            var UserSurvey = await _surveyManagementService.GetAssessmentSurvey(command);
            var expectedlevel = await _userManagementService.GetExpectedLevels();
            var userdetails = await _userManagementService.GetUsers();
            var usdata = UserSurvey.ToList();
            var reqlist = command.UserSurvey.ToList();

            //List<AssessmentSurveyVm> lst = new List<AssessmentSurveyVm>();
            //foreach (var item in reqlist)
            //{
            //    AssessmentSurveyVm asv = new AssessmentSurveyVm();
            //    asv.Id = item.Id;
            //    lst.Add(asv);
            //}
            //var userAssesseenamedata = usdata.Where(b => lst.Any(a => a.Id == b.ID)).ToList();
            var user = from us in userdetails
                       join SL in usdata on us.Id equals SL.AssesseeUId
                       select new
                       {
                           usersurveyid = SL.ID,
                           UId = us.Id,
                           UserId = us.UserId,
                           Role = us.Role,
                           FirstName = us.FirstName,
                           LastName = us.LastName
                       };
            var userlist = user.ToList();
            var ExpectedValueList = expectedlevel.ToList();
            AssessmentSurveyResponseVM assessmentSurveyResponseVM = new AssessmentSurveyResponseVM();
            List<AssessmentSurveyResVM> assessmentSurveyResVM = new List<AssessmentSurveyResVM>();
            for (int i = 0; i < userlist.Count; i++)
            {
                CompetenciesMapList competenciesMaplst = new CompetenciesMapList();
                //for (int j = 0; j < ExpectedValueList.Count; j++)
                //{
                List<CompetenciesMap> CompentcyMap = new List<CompetenciesMap>();
                //if (userlist[i].Role.CompetenciesMap.ToList()[0].ExpectedLevelId == ExpectedValueList[j].Id)
                //{

                foreach (var rol in userlist[i].Role.CompetenciesMap)
                {

                    var compmaplist = new CompetenciesMap()
                    {
                        competencyGroupId = rol.CompetencyGroupId,
                        expectedLevelId = rol.ExpectedLevelId,
                        competencyId = rol.CompetencyId,
                        isSelected = rol.IsSelected
                    };
                    CompentcyMap.Add(compmaplist);
                }
                competenciesMaplst.competenciesMap = CompentcyMap;

                var userValues = new AssessmentSurveyResVM()
                {
                    usersurveyid = userlist[i].usersurveyid,
                    Id = userlist[i].UId,
                    EmployeeName = userlist[i].FirstName + " " + userlist[i].LastName,
                    //BenchMarkId = userlist[i].Role.CompetenciesMap.ToList()[0].ExpectedLevelId,
                    //BenchMark = ExpectedValueList[j].ExpectedLevelName,
                    UserId = userlist[i].UserId,
                    RoleName = userlist[i].Role.RoleName,
                    RoleId = userlist[i].Role.RoleId,
                    RoleTId = userlist[i].Role.Id,
                    competenciesMap = competenciesMaplst
                };
                assessmentSurveyResVM.Add(userValues);
                //}
                //}
            }
            assessmentSurveyResponseVM.AssessmentUserSurvey = assessmentSurveyResVM;
            var usersurvey = assessmentSurveyResponseVM.AssessmentUserSurvey.ToList();
            List<AssessmentUserSurvey> assessmentusersurvey = new List<AssessmentUserSurvey>();
            var competencyDetails = await _competencyFrameworkService.GetCompetencyGroup(tkn);
            var compdetlist = competencyDetails.ToList();
            foreach (var usersur in usersurvey)
            {
                var assessmentsurvydata= await _surveyManagementService.GetAssessmentSurveyByUsersurveyId(usersur.usersurveyid);
                AssessmentUserSurvey aus = new AssessmentUserSurvey();
                aus.id = usersur.Id;
                aus.usersurveyid = usersur.usersurveyid;
                aus.userId = usersur.UserId;
                aus.employeeName = usersur.EmployeeName;
                aus.roleId = usersur.RoleId;
                aus.roleName = usersur.RoleName;
                //aus.benchMarkId = usersur.BenchMarkId;
                //aus.benchMark = usersur.BenchMark;
                aus.roleTId = usersur.RoleTId;
                //aus.actualId = 0;
                //aus.actualLevel = "";
                var competencyList = usersur.competenciesMap.competenciesMap.Select(s => (long)s.competencyGroupId).ToList().Distinct().ToList();
                var cmpdet = from listOfItems in competencyDetails
                             from item in competencyList
                             where item == listOfItems.Id
                             select listOfItems;
                List<CompetencyGroupsUserSurveyVm> cg = new List<CompetencyGroupsUserSurveyVm>();
                foreach (var item in cmpdet)
                {
                    CompetencyGroupsUserSurveyVm _competencyGroup = new CompetencyGroupsUserSurveyVm();
                    _competencyGroup.Id = item.Id;
                    _competencyGroup.CompetencyGroupId = item.CompetencyGroupId;
                    _competencyGroup.Name = item.Name;
                    _competencyGroup.Description = item.Description;
                    List<CompetenciesUserSurveyVm> _CompetencyList = new List<CompetenciesUserSurveyVm>();
                    foreach (var comde in usersur.competenciesMap.competenciesMap)
                    {
                        if (item.Id == comde.competencyGroupId)
                        {
                            foreach (var comp in item.Competencies)
                            {
                                if (comde.competencyId == comp.Id)
                                {
                                    CompetenciesUserSurveyVm competency = new CompetenciesUserSurveyVm();
                                    competency.Id = comp.Id;
                                    competency.CompetencyId = comp.CompetencyId;
                                    competency.Name = comp.Name;
                                    competency.Description = comp.Description;
                                    for (int j = 0; j < ExpectedValueList.Count; j++)
                                    {
                                        if (comde.expectedLevelId == ExpectedValueList[j].Id)
                                        {
                                            competency.BenchMarkId = comde.expectedLevelId;
                                            competency.BenchMark = ExpectedValueList[j].ExpectedLevelName;
                                            
                                        }
                                        
                                    }
                                    for (int k = 0; k < assessmentsurvydata.Count; k++)
                                    {
                                        if (comde.competencyId == assessmentsurvydata[k].CompetencyId)
                                        {
                                            for (int m = 0; m < ExpectedValueList.Count; m++)
                                            {
                                                if (ExpectedValueList[m].Id == assessmentsurvydata[k].ActualLevelId)
                                                {
                                                    competency.ActualId = ExpectedValueList[m].Id;
                                                    competency.ActualBenchMark = ExpectedValueList[m].ExpectedLevelName;
                                                }
                                            }
                                        }
                                    }


                                    //List<AttributesVm> _Attribute = new List<AttributesVm>();
                                    //foreach (var Attri in comp.Attributes)
                                    //{
                                    //    AttributesVm Attribute = new AttributesVm();
                                    //    Attribute.Id = Attri.Id;
                                    //    Attribute.Description = Attri.Description;
                                    //    Attribute.CompetencyId = Attri.CompetencyId;
                                    //    CompetencyLevelVm CompetencyLevel = new CompetencyLevelVm();
                                    //    CompetencyLevel.Id = Attri.Id;
                                    //    CompetencyLevel.Name = Attri.Description;
                                    //    CompetencyLevel.Weightage = Attri.CompetencyId;
                                    //    Attribute.CompetencyLevel = CompetencyLevel;
                                    //    _Attribute.Add(Attribute);
                                    //}
                                    //competency.Attributes = _Attribute;
                                    _CompetencyList.Add(competency);
                                }
                            }

                        }
                        _competencyGroup.Competencies = _CompetencyList;
                    }
                    cg.Add(_competencyGroup);

                }
                aus.CompetenciesMap = cg;
                assessmentusersurvey.Add(aus);
            }
            return Ok(assessmentusersurvey);
        }

        [HttpPost("CreateAssessmentSurvey")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateAssessmentSurvey(CreateAssessmentSurveyCommand command)
        {
            var tkn = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var competencyGroups = await _competencyFrameworkService.GetCompetencyGroup(tkn);
            var roles = await _userManagementService.GetRoles(tkn);
            var expectedlevel = await _userManagementService.GetExpectedLevels();
            var userdetails = await _userManagementService.GetUsers();
            List<CaptureAssessmentSurveyResponseVM> CreateAssessmentSurvey = new List<CaptureAssessmentSurveyResponseVM>();
            var compentency = competencyGroups.ToList();
            List<CompentencyGroupsCompetencyVM> cgc = new List<CompentencyGroupsCompetencyVM>();
            for (int c = 0; c < compentency.Count; c++)
            {
                var compentdet = compentency[c].Competencies.ToList();
                for (int cm = 0; cm < compentdet.Count; cm++)
                {
                    CompentencyGroupsCompetencyVM cgcm = new CompentencyGroupsCompetencyVM();
                    cgcm.CompetencyGroupId = compentency[c].Id;
                    cgcm.CompentencyId = compentdet[cm].Id;
                    cgc.Add(cgcm);
                }
            }


            var notinuser = command.assessmentsurvey.ToList().Where(b => userdetails.All(a => a.Id != b.UserId)).FirstOrDefault();
            if (notinuser != null)
            {
                CaptureAssessmentSurveyResponseVM cg = new CaptureAssessmentSurveyResponseVM();
                cg.UserSurveyAssessmentId = notinuser.UserSurveyAssessmentId;
                cg.Remarks = "User id Not Exists";
                CreateAssessmentSurvey.Add(cg);
                command.assessmentsurvey.Remove(notinuser);
            }
            var notincompmap = command.assessmentsurvey.ToList().Where(b => competencyGroups.All(a => a.Id != b.CompetencyGroupId)).FirstOrDefault();
            if (notincompmap != null)
            {
                CaptureAssessmentSurveyResponseVM cg = new CaptureAssessmentSurveyResponseVM();
                cg.UserSurveyAssessmentId = notincompmap.UserSurveyAssessmentId;
                cg.Remarks = "Compentency Group id Not Exists";
                CreateAssessmentSurvey.Add(cg);
                command.assessmentsurvey.Remove(notincompmap);
            }
            var notinrole = command.assessmentsurvey.ToList().Where(b => roles.All(a => a.Id != b.RoleId)).FirstOrDefault();
            if (notinrole != null)
            {
                CaptureAssessmentSurveyResponseVM cg = new CaptureAssessmentSurveyResponseVM();
                cg.UserSurveyAssessmentId = notinrole.UserSurveyAssessmentId;
                cg.Remarks = "Role Id Not Exists";
                CreateAssessmentSurvey.Add(cg);
                command.assessmentsurvey.Remove(notinrole);
            }
            var notinbenchmark = command.assessmentsurvey.ToList().Where(b => expectedlevel.All(a => a.Id != b.BenchMarkId)).FirstOrDefault();
            if (notinbenchmark != null)
            {
                CaptureAssessmentSurveyResponseVM cg = new CaptureAssessmentSurveyResponseVM();
                cg.UserSurveyAssessmentId = notinbenchmark.UserSurveyAssessmentId;
                cg.Remarks = "BenchMark Id Not Exists";
                CreateAssessmentSurvey.Add(cg);
                command.assessmentsurvey.Remove(notinbenchmark);
            }
            var notinActualmark = command.assessmentsurvey.ToList().Where(b => expectedlevel.All(a => a.Id != b.ActualLevelId)).FirstOrDefault();
            if (notinActualmark != null)
            {
                CaptureAssessmentSurveyResponseVM cg = new CaptureAssessmentSurveyResponseVM();
                cg.UserSurveyAssessmentId = notinActualmark.UserSurveyAssessmentId;
                cg.Remarks = "Actual Level Id Not Exists";
                CreateAssessmentSurvey.Add(cg);
                command.assessmentsurvey.Remove(notinActualmark);
            }

            var notincomptency = command.assessmentsurvey.ToList().Where(b => cgc.All(a => a.CompentencyId != b.CompetencyId)).FirstOrDefault();
            if (notincomptency != null)
            {
                CaptureAssessmentSurveyResponseVM cg = new CaptureAssessmentSurveyResponseVM();
                cg.UserSurveyAssessmentId = notincomptency.UserSurveyAssessmentId;
                cg.Remarks = "Compentency Id Not Exists";
                CreateAssessmentSurvey.Add(cg);
                command.assessmentsurvey.Remove(notincomptency);
            }
            if (command.assessmentsurvey.Count != 0)
            {
                var Asseementsurvey = new List<CaptureAssessmentSurveyResponseVM>();
                Asseementsurvey = await _surveyManagementService.CreateAssessmentSurvey(command);
            }
            //List<UpdateUserSurveyAssessment> uusalst = new List<UpdateUserSurveyAssessment>();
            //foreach(var item in Asseementsurvey)
            //{
            //    UpdateUserSurveyAssessment uusa = new UpdateUserSurveyAssessment();
            //    uusa.Id = item.UserSurveyAssessmentId;
            //    uusa.AssessmentTypeId = item.AssessmentTypeId;
            //    uusalst.Add(uusa);
            //}
            //UpdateUserSurveyAssessmentCommand uusac = new UpdateUserSurveyAssessmentCommand();
         
            //uusac.UpdateAssessment= uusalst;
            //var updateuserassementstatus = await _surveyManagementService.UpdateUserSurveyAssessment(uusac);
            return Ok(CreateAssessmentSurvey);
        }
    }
}
