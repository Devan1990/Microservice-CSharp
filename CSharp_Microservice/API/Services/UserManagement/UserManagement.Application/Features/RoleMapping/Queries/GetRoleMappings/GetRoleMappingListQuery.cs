using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping
{
    public class GetRoleMappingListQuery : IRequest<List<RoleMappingVm>>
    {
        public GetRoleMappingListQuery()
        {

        }
    }
}
