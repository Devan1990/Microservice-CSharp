using AutoMapper;
using SurveyManagement.Application.Features.AssessmentSurvey.Commands.CreateAssessmentSurvey;
using SurveyManagement.Application.Features.AssessmentSurvey.Commands.UpdateAssessmentSurvey;
using SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey;
using SurveyManagement.Application.Features.Survey.Commands.CreateSurvey;
using SurveyManagement.Application.Features.Survey.Commands.UpdateSurvey;
using SurveyManagement.Application.Features.Survey.Queries.GetSurvey;
using SurveyManagement.Application.Features.UsersSurvey.Commands.UpdateUserSurvey;
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey;
using SurveyManagement.Domain.Entities;

namespace SurveyManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {



            CreateMap<Survey, SurveyVm>().ReverseMap();
            CreateMap<Survey, CreateSurveyCommand>().ReverseMap();
            CreateMap<AssessmentSurvey, CreateAssessmentSurveyCommand>().ReverseMap();
            CreateMap<AssessmentSurvey, CaptureAssessmentSurveyVm>().ReverseMap();
            CreateMap<AssessmentSurvey, CaptureAssessmentSurveyVm2>().ReverseMap();
          //  CreateMap<SurveyRoleMapping, SurveyRoleMappingVm>().ReverseMap();
            CreateMap<Survey, UpdateSurveyCommand>().ReverseMap();
           // CreateMap<SurveyRoleMapping, UpdateSurveyRoleMapVm>().ReverseMap();
            CreateMap<AssessmentSurvey, UpdateAssessmentSurveyCommand>().ReverseMap();
            CreateMap<AssessmentSurvey, UpdateAssessmentSurvey>().ReverseMap();
            CreateMap<AssessmentSurvey, UpdateAssessmentSurveyVm>().ReverseMap();
            CreateMap<UserSurvey, UserSurveyVm>().ReverseMap();
            CreateMap<UserSurveyAssessment, UserSurveyAssessmentVm>().ReverseMap();
            CreateMap<UserSurvey, UserSurveyVm2>().ReverseMap();
            CreateMap<UserSurveyAssessment, UpdateUserSurveyAssessmentVm>().ReverseMap();
            CreateMap<UserSurveyAssessment, UpdateUserSurveyAssessmentCommand>().ReverseMap();
            CreateMap<AssessmentType, AssessmenttypeVm>().ReverseMap();
            CreateMap<UpdateUserSurveyAssessment, UpdateUserSurveyAssessmentVm>().ReverseMap();
            CreateMap<UserSurveyAssessment, UpdateUserSurveyAssessment>().ReverseMap();
            CreateMap<UserSurvey, UserSurveyAssessmentTypeVM>().ReverseMap();
            CreateMap<UserSurvey, UserSurveyVm>()
           .ForMember(x => x.UserSurveyAssessments, x => x.MapFrom(a => a.UserSurveyAssessments));
            CreateMap<UserSurveyAssessment, UserSurveyAssessmentVm>()
            .ForMember(x => x.AssessmentTypeId, x => x.MapFrom(a => a.AssessmentType));
            CreateMap<UserSurvey, AssessmenttypeVm>().ReverseMap();
            CreateMap<UserSurveyAssessment, AssessmenttypeVm>().ReverseMap();
          
            CreateMap<AssessmentType, UserSurveyAssessmentTypeVM>().ReverseMap();
            CreateMap<UserSurveyAssessment, UserSurveyAssessmentSurveyVm>().ReverseMap();
            CreateMap<UserSurveyAssessment, UserSurveyAssessmentSurveyVm>()
                .ForMember(x => x.UserSurveyId, x => x.MapFrom(a => a.UserSurvey));
            CreateMap<UserSurvey, AssessmentUserSurvVM>().ReverseMap();
            CreateMap<UserSurveyAssessment, AssessmentUserSurvVM>()
            .ForMember(x => x.SurveyId, x => x.MapFrom(a => a.UserSurvey));
            CreateMap<UserSurveyAssessment, CaptureAssessmentSurveyVm2>().ReverseMap();
            CreateMap<AssessmentSurvey, AssessmentSurveysVm>().ReverseMap();
            CreateMap<AssessmentType, AssessmentTypeVM>().ReverseMap();

            //     .ForMember(x => x.Id, x => x.MapFrom(a => a.Id))
            //     .ForMember(x => x.RoleId, x => x.MapFrom(a => a.RoleId))
            //     .ForMember(x => x.RoleName, x => x.MapFrom(a => a.RoleName));


        }
    }
}


