


using AutoMapper;
using UserManagement.Domain.Entities;
using System.Linq;
using UserManagement.Application.Features.Role.Queries.GetRoles;
using UserManagement.Application.Features.Role.Commands.CreateRole;
using UserManagement.Application.Features.Role.Commands.UpdateRole;
using UserManagement.Application.Features.Users.Queries.GetUsers;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Features.Users.Commands.UpdateUser;
using UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping;
using UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping;
using UserManagement.Application.Features.RoleMapping.Commands.UpdateRoleMapping;

namespace UserManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            CreateMap<User, UsersVm>().ReverseMap()
                .ForMember(x => x.Role, x => x.Ignore());

            CreateMap<User, CreateUserCommand>().ReverseMap()
                .ForMember(x => x.Role, x => x.Ignore())
                .ForMember(x => x.Vertical, x => x.Ignore());

            CreateMap<Country, CountryVm>().ReverseMap().ForMember(x => x.Areas, x => x.MapFrom(a => a.Areas.ToList()));
            CreateMap<Country, UserCountryVm>().ReverseMap();
            CreateMap<Area, UserAreaVm>().ReverseMap();

            CreateMap<Area, AreaVm>().ReverseMap().ForMember(x => x.Regions, x => x.MapFrom(a => a.Regions.ToList()));

            CreateMap<Region, RegionVm>().ReverseMap();

            CreateMap<Verticals, VerticalVm>().ReverseMap();

            CreateMap<RoleType, RoleTypeVm>().ReverseMap();

            CreateMap<User, UpdateUserCommand>().ReverseMap();

            CreateMap<User, UsersVm>()
           .ForMember(x => x.Role, x => x.MapFrom(a => a.Role))
            .ForMember(x => x.Country, x => x.MapFrom(a => a.Country))
            .ForMember(x => x.Region, x => x.MapFrom(a => a.Region))
            .ForMember(x => x.Area, x => x.MapFrom(a => a.Area))
            .ForMember(x => x.Vertical, x => x.MapFrom(a => a.Vertical));


            CreateMap<Country, CountryVm>().ReverseMap()
              .ForMember(x => x.Areas, x => x.MapFrom(a => a.Areas.ToList()));


             




            CreateMap<Role, RoleVm>().ReverseMap();
            CreateMap<Role, RoleVm2>().ForMember(x => x.RoleType, x => x.MapFrom(a => a.RoleType));
            CreateMap<CompetenciesMap, CreateRoleCompetenciesMapVm>().ReverseMap();
            CreateMap<CompetenciesMap, UpdateRoleCompetenciesMapVm>().ReverseMap();
            CreateMap<Role, CreateRoleCommand>().ReverseMap().ForMember(x => x.RoleType, x => x.Ignore());
            CreateMap<Role, UpdateRoleCommand>().ReverseMap();

            CreateMap<Role, RoleVm>().ForMember(x => x.RoleType, x => x.MapFrom(a => a.RoleType))
                                     .ForMember(x => x.CompetenciesMap, x => x.MapFrom(a => a.CompetenciesMap.ToList()));
                                     

            CreateMap<Role, CreateRoleCompetenciesMapVm>();
            CreateMap<ExpectedLevel, ExpectedLevelVm>().ReverseMap();

            CreateMap<RoleMapping, RoleMappingVm>().ReverseMap();
            CreateMap<RoleMapping, RoleMappingVm>().ReverseMap().ForMember(x => x.AssessorRole, x => x.MapFrom(a => a.AssessorRole.ToList()));
            CreateMap<AssessorRole, CreateRoleMappingAssessorRolesVM>().ReverseMap();
            CreateMap<RoleMapping, CreateRoleMappingCommand>().ReverseMap();
            CreateMap<RoleMapping, CreateRoleMappingAssessorRolesVM>();

            //CreateMap<RoleMapping, RoleMappingVm>().ReverseMap().ForMember(x => x.AssessorRole, x => x.MapFrom(a => a.AssessorRole.ToList()));
            CreateMap<AssessorRole, AssessorRoleVm>().ReverseMap();
            CreateMap<AssessorRole, UpdateRoleMappingAssessorRolesVM>().ReverseMap();
            CreateMap<RoleMapping, UpdateRoleMappingCommand>().ReverseMap();
            CreateMap<RoleMapping, UpdateRoleMappingAssessorRolesVM>();
            CreateMap<CompetenciesMap, GetRoleCompetenciesMapVm>().ReverseMap();
            CreateMap<Role, GetRoleCompetenciesMapVm>();

        }
    }
}
