using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping
{
    public class AssessorRoleVm
    {
        public long Id { get; set; }
        public bool Mandatory { get; set; }
        public long RoleId { get; set; }
        public string RoleBId { get; set; }
        public string RoleName { get; set; }
    }
}
