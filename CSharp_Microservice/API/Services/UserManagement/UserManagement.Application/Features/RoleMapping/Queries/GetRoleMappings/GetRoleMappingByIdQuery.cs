using MediatR;
using System;
using System.Collections.Generic;


namespace UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping
{
    public class GetRoleMappingByIdQuery : IRequest<RoleMappingVm>
    {
        public long Id { get; set; }

        public GetRoleMappingByIdQuery(long id)
        {
            Id = id;
        }
    }
}
