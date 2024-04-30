using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping;

namespace UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping
{
    public class CreateRoleMappingCommand : IRequest<long>
    {
         public DateTime AssessmentPeriodFrom { get; set; }
        public DateTime AssessmentPeriodTo { get; set; }
        public long RoleId { get; set; }
        //public string RoleName { get; set; }
      //  public long RoleTypeId { get; set; }
        public ICollection<CreateRoleMappingAssessorRolesVM> AssessorRole { get; set; }

    }
}
