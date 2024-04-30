using AutoMapper;
using EventBus.Messages.Events;
using UserManagement.Application.Features.Role.Commands.CreateRole;
using UserManagement.Application.Features.Users.Commands.CreateUser;

namespace CompetencyFramework.API.Mapper
{
    public class UserManagementProfile : Profile
	{
		public UserManagementProfile()
		{
			CreateMap<CreateUserCommand, BasketCheckoutEvent>().ReverseMap();
			CreateMap<CreateRoleCommand, BasketCheckoutEvent>().ReverseMap();
		}
	}
}
