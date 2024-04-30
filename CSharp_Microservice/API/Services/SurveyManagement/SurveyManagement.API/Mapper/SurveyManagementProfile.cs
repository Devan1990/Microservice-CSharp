using AutoMapper;
using EventBus.Messages.Events;
using SurveyManagement.Application.Features.Survey.Commands.CreateSurvey;


namespace SurveyManagement.API.Mapper
{
    public class SurveyManagementProfile : Profile
	{
		public SurveyManagementProfile()
		{
			CreateMap<CreateSurveyCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
