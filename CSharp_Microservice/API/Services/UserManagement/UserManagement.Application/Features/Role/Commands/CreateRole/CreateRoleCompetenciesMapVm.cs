using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCompetenciesMapVm
    {
      
        public long CompetencyGroupId { get; set; }
        public long CompetencyId { get; set; }
        public bool IsSelected { get; set; } = false;
        public long ExpectedLevelId { get; set; }
    }
}
