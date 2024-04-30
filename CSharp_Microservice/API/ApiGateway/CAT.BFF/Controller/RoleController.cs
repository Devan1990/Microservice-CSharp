using CAT.BFF.Models;
using CAT.BFF.Services;
using CATBFF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace CAT.BFF.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ICompetencyFrameworkService _competencyFrameworkService;
        private readonly IUserManagementService _userManagementService;

        public RoleController(ICompetencyFrameworkService competencyFrameworkService, IUserManagementService userManagementService)
        {
            _competencyFrameworkService = competencyFrameworkService ?? throw new ArgumentNullException(nameof(competencyFrameworkService));
            _userManagementService = userManagementService ?? throw new ArgumentNullException(nameof(userManagementService));
        }

        [HttpGet("GetRoles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRoles()
        {
            var tkn = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var cg = await _competencyFrameworkService.GetCompetencyGroup(tkn);
            var roles = await _userManagementService.GetRoles(tkn);
            var result = roles.Select(x => new
            {
                x.Id,
                x.RoleId,
                x.RoleName,
                x.RoleGuid,
                x.RoleDescription,
                x.RoleType,
                CompetencyGroups = x.CompetenciesMap.Select(y => new
                {

                    Id = cg.Where(w => w.Id == y.CompetencyGroupId).Select(s => s.Id).FirstOrDefault(),
                    CompetencyGroupId = cg.Where(w => w.Id == y.CompetencyGroupId).Select(s => s.CompetencyGroupId).FirstOrDefault(),
                    Name = cg.Where(w => w.Id == y.CompetencyGroupId).Select(s => s.Name).FirstOrDefault(),
                    Description = cg.Where(w => w.Id == y.CompetencyGroupId).Select(s => s.Description).FirstOrDefault(),
                    Status = cg.Where(w => w.Id == y.CompetencyGroupId).Select(s => s.Status).FirstOrDefault()



                }).ToList().Distinct()
            });
            
            
            
          
            return Ok(result);
        }

        [HttpGet("GetRole/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRole(long id)
        {
            var tkn = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var roles = await _userManagementService.GetRole(id);
            var competencyDetails = await _competencyFrameworkService.GetCompetencyGroup(tkn);
            var compdetlist = competencyDetails.ToList();
            List<RoleVM> roleList = new List<RoleVM>();
            RoleVM rm = new RoleVM();
            rm.Id = roles.Id;
            rm.RoleId = roles.RoleId;
            rm.RoleGuid = roles.RoleGuid;
            rm.RoleDescription = roles.RoleDescription;
            rm.RoleName = roles.RoleName;
            rm.RoleType = roles.RoleType;
            var competencyList = roles.CompetenciesMap.Select(s => (long)s.CompetencyGroupId).ToList().Distinct().ToList();
            var cmpdet = from listOfItems in competencyDetails
                         from item in competencyList
                         where item == listOfItems.Id
                         select listOfItems;
            //select new {

            //    Id = listOfItems.Id, 
            //             CompetencyGroupId = listOfItems.CompetencyGroupId };
            var expectedlevel = await _userManagementService.GetExpectedLevels();
            var ExpectedValueList = expectedlevel.ToList();
            List<CompetencyGroupsRolesVm> cg = new List<CompetencyGroupsRolesVm>();
            foreach (var item in cmpdet)
            {
                CompetencyGroupsRolesVm _competencyGroup = new CompetencyGroupsRolesVm();
                _competencyGroup.Id = item.Id;
                _competencyGroup.CompetencyGroupId = item.CompetencyGroupId;
                _competencyGroup.Name = item.Name;
                _competencyGroup.Description = item.Description;
                _competencyGroup.Status = item.Status;
                List<CompetenciesRoleVm> _CompetencyList = new List<CompetenciesRoleVm>();
                foreach (var comde in roles.CompetenciesMap)
                {
                    if (item.Id == comde.CompetencyGroupId)
                    {
                        foreach (var comp in item.Competencies)
                        {
                            if (comde.CompetencyId == comp.Id)
                            {
                                CompetenciesRoleVm competency = new CompetenciesRoleVm();
                                competency.CompetencyMapId = comde.Id;
                                competency.Id = comp.Id;
                                competency.CompetencyId = comp.CompetencyId;
                                competency.Name = comp.Name;
                                competency.Description = comp.Description;
                                competency.IsSelected = comde.IsSelected;
                                // competency.
                                for (int j = 0; j < ExpectedValueList.Count; j++)
                                {
                                    if (comde.ExpectedLevelId == ExpectedValueList[j].Id)
                                    {
                                        if (comde.IsSelected == true)
                                        {
                                            competency.ExpectedLevelId = comde.ExpectedLevelId;
                                            competency.ExpectedLevelName = ExpectedValueList[j].ExpectedLevelName;
                                        }
                                        else
                                        {
                                            competency.ExpectedLevelId = 0;
                                            competency.ExpectedLevelName = "";
                                        }
                                    }
                                }
                                List<AttributesVm> _Attribute = new List<AttributesVm>();
                                foreach (var Attri in comp.Attributes)
                                {
                                    AttributesVm Attribute = new AttributesVm();
                                    CompetencyLevelVm CompetencyLevel = new CompetencyLevelVm();
                                    CompetencyLevel.Id = Attri.CompetencyLevel.Id;
                                    CompetencyLevel.Name = Attri.CompetencyLevel.Name;
                                    CompetencyLevel.Weightage = Attri.CompetencyLevel.Weightage;
                                    Attribute.CompetencyLevel = CompetencyLevel;
                                    Attribute.Id = Attri.Id;
                                    Attribute.Description = Attri.Description;
                                    Attribute.CompetencyId = Attri.CompetencyId;
                                    _Attribute.Add(Attribute);
                                }
                                competency.Attributes = _Attribute;
                                _CompetencyList.Add(competency);
                            }
                        }

                    }
                    _competencyGroup.Competencies = _CompetencyList;
                }
                cg.Add(_competencyGroup);
            }
            rm.CompetenciesMap = cg;
            roleList.Add(rm);
            return Ok(roleList);
        }

    }
}
