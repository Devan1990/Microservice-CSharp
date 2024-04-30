using CAT.BFF.Models;
using CATBFF.Models;

namespace CAT.BFF.Services
{
    public interface IUserManagementService
    {
        Task<IEnumerable<RoleVm2>> GetRoles(string token);
        Task<IEnumerable<ExpectedLevelVm>> GetExpectedLevels();

        Task<IEnumerable<UsersVm>> GetUsers();

        Task<RoleVm2> GetRole(long Id);
    }
}
