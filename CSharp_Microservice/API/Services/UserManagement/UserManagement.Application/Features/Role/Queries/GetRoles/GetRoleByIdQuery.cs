using MediatR;
using System;
using System.Collections.Generic;
using UserManagement.Application.Features.Role.Queries.GetRoles;

namespace UserManagement.Application.Features.Role.Queries.GetUsers
{
    public class GetRoleByIdQuery : IRequest<RoleVm2>
    {
        public long Id { get; set; }

        public GetRoleByIdQuery(long id)
        {
            Id = id;
        }
    }
}
