using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping;

namespace UserManagement.Application.Features.RoleMapping.Commands.UpdateRoleMapping
{
    public class UpdateRoleMappingCommand : IRequest
    {
        public long Id { get; set; }
         public DateTime AssessmentPeriodFrom { get; set; }
        public DateTime AssessmentPeriodTo { get; set; }
        public long RoleId { get; set; }
        //public string RoleName { get; set; }
      //  public long RoleTypeId { get; set; }
        public ICollection<UpdateRoleMappingAssessorRolesVM> AssessorRole { get; set; }

    }
}
