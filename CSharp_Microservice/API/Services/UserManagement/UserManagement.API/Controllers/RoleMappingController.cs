using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping;
using UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping;
using UserManagement.Application.Features.RoleMapping.Commands.UpdateRoleMapping;
using UserManagement.Application.Contracts.Persistence;




namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleMappingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRoleMappingRepository _rolemappingrepository;
        private readonly IRoleRepository _rolerepository;

        public RoleMappingController(IMediator mediator, IRoleMappingRepository roleMappingRepository, IRoleRepository roleRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _rolemappingrepository = roleMappingRepository ?? throw new ArgumentNullException(nameof(roleMappingRepository));
            _rolerepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        // testing purpose
        [HttpPost("CreateRoleMapping")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateRoleMapping([FromBody] CreateRoleMappingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetRoleMappings")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRoleMappings()
        {
            var command = new GetRoleMappingListQuery();
            var roles = await _mediator.Send(command);
            if (roles == null)
            {
                return NotFound();
            }
            var Rolevalues = await _rolerepository.GetRoles();
            var rolelist = Rolevalues.ToList();
            for (int i = 0; i < roles.Count; i++)
            {
                for (int m = 0; m < rolelist.Count; m++)
                {
                    if (roles[i].RoleId == rolelist[m].Id)
                    {
                        roles[i].RoleBId = rolelist[m].RoleId;
                        roles[i].RoleName = rolelist[m].RoleName;
                    }
                }
                var APRlst = roles[i].AssessorRole.ToList();
                List<AssessorRoleVm> assessorRolelst = new List<AssessorRoleVm>();
                for (int l = 0; l < APRlst.Count; l++)
                {
                    AssessorRoleVm assessorRoleVm = new AssessorRoleVm();
                    for (int j = 0; j < rolelist.Count; j++)
                    {
                        if (APRlst[l].RoleId == rolelist[j].Id)
                        {
                            assessorRoleVm.Id = APRlst[l].Id;
                            assessorRoleVm.RoleId = APRlst[l].RoleId;
                            assessorRoleVm.RoleBId = rolelist[j].RoleId;
                            assessorRoleVm.RoleName = rolelist[j].RoleName;
                            assessorRoleVm.Mandatory = APRlst[l].Mandatory;
                        }
                    }
                    assessorRolelst.Add(assessorRoleVm);
                }
                roles[i].AssessorRole = assessorRolelst;

            }
            return Ok(roles);
        }

        [HttpGet("GetRoleMappingById/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetRoleMappingById(long id)
        {
            var command = new GetRoleMappingByIdQuery(id);
            var roleMapping = await _mediator.Send(command);
            if (roleMapping == null)
            {
                return NotFound();
            }
            RoleMappingVm roles1 = new RoleMappingVm();
            List<RoleMappingVm> roles = new List<RoleMappingVm>();
            roles1.Id = roleMapping.Id;
            roles1.AssessmentPeriodFrom = roleMapping.AssessmentPeriodFrom;
            roles1.AssessmentPeriodTo = roleMapping.AssessmentPeriodTo;
            roles1.RoleId = roleMapping.RoleId;
            var ass = roleMapping.AssessorRole.ToList();
            List<AssessorRoleVm> assessorRoles = new List<AssessorRoleVm>();
            for (int a = 0; a < ass.Count; a++)
            {
                AssessorRoleVm ar = new AssessorRoleVm();
                ar.Mandatory = ass[a].Mandatory;
                ar.Id = ass[a].Id;
                ar.RoleId = ass[a].RoleId;
                ar.RoleBId = ass[a].RoleBId;
                ar.RoleName = ass[a].RoleName;
                assessorRoles.Add(ar);
            }
            roles1.AssessorRole = assessorRoles;
            roles.Add(roles1);
            var Rolevalues = await _rolerepository.GetRoles();
            var rolelist = Rolevalues.ToList();

            for (int i = 0; i < roles.Count; i++)
            {
                for (int m = 0; m < rolelist.Count; m++)
                {
                    if (roles[i].RoleId == rolelist[m].Id)
                    {
                        roles[i].RoleBId = rolelist[m].RoleId;
                        roles[i].RoleName = rolelist[m].RoleName;
                    }
                }
                var APRlst = roles[i].AssessorRole.ToList();
                List<AssessorRoleVm> assessorRolelst = new List<AssessorRoleVm>();
                for (int l = 0; l < APRlst.Count; l++)
                {
                    AssessorRoleVm assessorRoleVm = new AssessorRoleVm();
                    for (int j = 0; j < rolelist.Count; j++)
                    {
                        if (APRlst[l].RoleId == rolelist[j].Id)
                        {
                            assessorRoleVm.Id = APRlst[l].Id;
                            assessorRoleVm.RoleId = APRlst[l].RoleId;
                            assessorRoleVm.RoleBId = rolelist[j].RoleId;
                            assessorRoleVm.RoleName = rolelist[j].RoleName;
                            assessorRoleVm.Mandatory = APRlst[l].Mandatory;
                        }
                    }
                    assessorRolelst.Add(assessorRoleVm);
                }
                roles[i].AssessorRole = assessorRolelst;

            }
            return Ok(roles);

        }

        [HttpPut("UpdateRoleMapping")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateRoleMapping([FromBody] UpdateRoleMappingCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
