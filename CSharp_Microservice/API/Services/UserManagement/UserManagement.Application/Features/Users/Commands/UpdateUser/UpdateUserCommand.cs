using MediatR;

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public int AreaId { get; set; }
        public int RegionId { get; set; }
        public long RoleId { get; set; }
        public int VerticaId { get; set; }

    }
}
