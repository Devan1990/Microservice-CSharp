using System;
using System.Collections.Generic;
using UserManagement.Application.Features.Role.Queries.GetRoles;
//using UserManagement.Application.Features.Role.Queries.GetRoles

namespace UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping
{
    public class RoleMappingVm
    {
        public long Id { get; set; }
        public DateTime AssessmentPeriodFrom { get; set; }
        public DateTime AssessmentPeriodTo { get; set; }
        public long RoleId { get; set; }
        public string RoleBId { get; set; }
        public string RoleName { get; set; }
        
       // public RoleTypeVm RoleType { get; set; }
        public bool IsDeleted { get; set; } = false;
       public virtual ICollection<AssessorRoleVm> AssessorRole { get; set; }
    }
}