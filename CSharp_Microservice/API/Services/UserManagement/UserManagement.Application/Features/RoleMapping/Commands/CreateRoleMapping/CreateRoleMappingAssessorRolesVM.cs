using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping
{
    public class CreateRoleMappingAssessorRolesVM
    {
        public bool Mandatory { get; set; }
        public long RoleId { get; set; }
        //public string RoleName { get; set; }
    }
}
