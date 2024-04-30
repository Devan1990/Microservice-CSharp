using AutoMapper;
using CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute;
using CompetencyFramework.Application.Features.Attribute.Commands.DeleteAttribute;
using CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute;
using CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute;
using CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency;
using CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency;
using CompetencyFramework.Application.Features.Competency.Queries.GetCompetency;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.UpdateCompetencyGroup;
using CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup;
using CompetencyFramework.Domain.Entities;
using System.Linq;

namespace CompetencyFramework.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompetencyGroup, CompetencyGroupsVm>().ReverseMap();

            CreateMap<CompetencyGroup, CreateCompetencyGroupCommand>().ReverseMap();

            CreateMap<CompetencyGroup, UpdateCompetencyGroupCommand>().ReverseMap();

            CreateMap<Competency, CompetenciesVm>().ReverseMap()
                .ForMember(x => x.CompetencyGroup, x => x.Ignore());

            CreateMap<Competency, CreateCompetencyCommand>().ReverseMap()
                .ForMember(x => x.CompetencyGroup, x => x.Ignore());

            CreateMap<Competency, UpdateCompetencyCommand>().ReverseMap();
            
            CreateMap<Competency, CompetenciesVm>()
                .ForMember(x => x.Attributes, x => x.MapFrom(a => a.Attributes));

            CreateMap<CompetencyGroup, CompetencyGroupsVm>()
                .ForMember(x => x.Competencies, x => x.MapFrom(a => a.Competencies.ToList()));

            CreateMap<CompetencyLevel, CompetencyLevelVm>().ReverseMap();

            CreateMap<Attribute, AttributesVm>()
                            .ForMember(x => x.CompetencyId, x => x.MapFrom(a => a.Competency.Id))
                            .ForMember(x => x.CompetencyLevel, x => x.Ignore());

            CreateMap<Attribute, CreateAttributeCommand>().ReverseMap()
                .ForMember(x => x.CompetencyLevel, x => x.Ignore());

            CreateMap<Attribute, AttributesVm>()
               .ForMember(x => x.CompetencyLevel, x => x.MapFrom(a => a.CompetencyLevel));

            CreateMap<Attribute, UpdateAttributeCommand>().ReverseMap();
            CreateMap<Competency, CreateCompetencyGroupCommand>().ReverseMap();
            CreateMap <Competency, CreateCGCompetencyCommand>().ReverseMap();
            CreateMap<Attribute, CreateCompentencyGroupAttributeCommand>().ReverseMap();
            CreateMap<Competency, UpdateCGCompetencyCommand>().ReverseMap();
            CreateMap<Attribute, UpdateCGAttributeCommand>().ReverseMap();
        }
    }
}
