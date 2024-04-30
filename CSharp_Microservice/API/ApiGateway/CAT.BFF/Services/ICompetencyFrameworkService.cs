using CATBFF.Models;

namespace CAT.BFF.Services
{
    public interface ICompetencyFrameworkService
    {
        Task<IEnumerable<CompetencyGroupsVm>> GetCompetencyGroup(string tkn);
    }
}
