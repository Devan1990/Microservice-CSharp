using MediatR;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId  { get; set; }
        public int AreaId { get; set; }
        public int RegionId { get; set; }
        public long RoleId { get; set; }
        public int VerticaId { get; set; }
    }
}
