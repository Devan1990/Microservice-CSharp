using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Features.Role.Commands.CreateRole
{
    public class UpdateRoleCompetenciesMapVm
    {
        public long Id { get; set; }
        public int CompetencyGroupId { get; set; }
        public int CompetencyId { get; set; }
        public bool IsSelected { get; set; } = true;
        public long ExpectedLevelId { get; set; }
        
        [Display(Order = -1)]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
