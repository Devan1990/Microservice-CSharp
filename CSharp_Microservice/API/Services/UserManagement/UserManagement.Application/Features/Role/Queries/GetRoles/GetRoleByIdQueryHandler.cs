using AutoMapper;
using CompetencyFramework.API.GrpcServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Features.Role.Queries.GetRoles;
using UserManagement.Application.Features.Role.Commands.CreateRole;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Role.Queries.GetUsers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleVm2>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly CompetencyFrameworkGrpcService _competencyFrameworkGrpcService;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper, CompetencyFrameworkGrpcService competencyFrameworkGrpcService)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _competencyFrameworkGrpcService = competencyFrameworkGrpcService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RoleVm2> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleById(request.Id);
            if(role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var competencyList = role.CompetenciesMap.Select(s => (long)s.CompetencyGroupId).ToList().Distinct().ToList();
            var competencyDetails = await _competencyFrameworkGrpcService.GetCompetencyDetails(competencyList);
            var competencymapList = role.CompetenciesMap.ToList();
            
            var ExpectedValue = await _roleRepository.GetExpectedLevels();
            var ExpectedValueList = ExpectedValue.ToList();
            List<Domain.Entities.Role> rolelist = new List<Domain.Entities.Role>();
            rolelist.Add(role);
            for (int r = 0; r < rolelist.Count; r++)
            {
                for (int i = 0; i < competencyDetails.CompetencyGroups.Count; i++)
                {
                    for (int c = 0; c < competencyDetails.CompetencyGroups[i].Competencies.Count; c++)
                    {
                        var rlst = rolelist[r].CompetenciesMap.ToList();
                        var notincompmap = competencyDetails.CompetencyGroups[i].Competencies.ToList().Where(b => rlst.All(a => a.CompetencyId != b.Id)).FirstOrDefault();
                        if (notincompmap != null)
                            competencyDetails.CompetencyGroups[i].Competencies.Remove(notincompmap);
                    }
                     
                    for (int j = 0; j < competencyDetails.CompetencyGroups[i].Competencies.Count; j++)
                    {
                        for (int m = 0; m < competencyList.Count; m++)
                        {
                            if (competencyDetails.CompetencyGroups[i].Competencies[j].Id == competencymapList[m].CompetencyId)
                            {
                                for (int e = 0; e < ExpectedValueList.Count; e++)
                                {
                                    if (competencymapList[m].ExpectedLevelId == ExpectedValueList[e].Id)
                                    {
                                        competencyDetails.CompetencyGroups[i].Competencies[j].ExpectedLevelName = ExpectedValueList[e].ExpectedLevelName;
                                        competencyDetails.CompetencyGroups[i].Competencies[j].ExpectedLevelID = ExpectedValueList[e].Id;
                                        competencyDetails.CompetencyGroups[i].Competencies[j].IsSelected = competencymapList[m].IsSelected;
                                    }
                                }
                            }
                        }


                    }
                }
            }
            var roletype = role;
            RoleTypeVm rt = new RoleTypeVm();
            RoleType role1 = new RoleType();
            role1 = role.RoleType;
            rt.RoleType = role1.Roletype;
            rt.Id = role1.Id;
            List<GetRoleCompetenciesMapVm> rcm=new List<GetRoleCompetenciesMapVm>();
            var cmlst = role.CompetenciesMap.ToList();
            for (int e = 0; e < cmlst.Count; e++)
            {
                GetRoleCompetenciesMapVm rcmdata = new GetRoleCompetenciesMapVm();
                rcmdata.Id = cmlst[e].Id;
                rcmdata.CompetencyGroupId = cmlst[e].CompetencyGroupId;
                rcmdata.CompetencyId= cmlst[e].CompetencyId;
                rcmdata.ExpectedLevelId = cmlst[e].ExpectedLevelId;
                rcmdata.IsSelected = cmlst[e].IsSelected;
                rcm.Add(rcmdata);
            }
            return new RoleVm2()
            {
                Id = role.Id,
                RoleDescription = role.RoleDescription,
                RoleGuid = role.RoleGuid,
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleType = rt,
                CompetenciesMap = rcm
            };
        }
        
    }

}
