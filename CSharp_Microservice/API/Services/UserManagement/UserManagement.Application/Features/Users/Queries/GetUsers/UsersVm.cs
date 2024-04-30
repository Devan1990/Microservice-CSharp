using UserManagement.Application.Features.Role.Queries.GetRoles;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class UsersVm
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserCountryVm Country { get; set; }
        public UserAreaVm Area { get; set; }
        public RegionVm Region { get; set; }
        public RoleVm2 Role { get; set; }
        public VerticalVm Vertical { get; set; }
    }
}
