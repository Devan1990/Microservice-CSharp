using AutoMapper;
using CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup;
using EventBus.Messages.Events;

namespace CompetencyFramework.API.Mapper
{
    public class CompetencyFrameworkProfile : Profile
	{
		public CompetencyFrameworkProfile()
		{
			CreateMap<CreateCompetencyGroupCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
