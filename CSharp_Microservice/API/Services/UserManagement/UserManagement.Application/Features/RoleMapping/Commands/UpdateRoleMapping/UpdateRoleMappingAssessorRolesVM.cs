using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Features.RoleMapping.Commands.UpdateRoleMapping
{
    public class UpdateRoleMappingAssessorRolesVM
    {
        public long Id { get; set; }
        public bool Mandatory { get; set; }
        public long RoleId { get; set; }
        // public string RoleName { get; set; }
        [DefaultValue(false)]
        [Display(Order = -1)]
        public bool IsDeleted { get; set; }
    }
}
