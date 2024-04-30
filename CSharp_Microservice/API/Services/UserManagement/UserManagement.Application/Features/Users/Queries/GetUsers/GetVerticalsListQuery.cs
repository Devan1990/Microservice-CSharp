using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetVerticalsListQuery : IRequest<List<VerticalVm>>
    {
        public GetVerticalsListQuery()
        {

        }
    }
}
