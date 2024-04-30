using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommand :IRequest
    {
        
            public int Id { get; set; }
        
    }
}
