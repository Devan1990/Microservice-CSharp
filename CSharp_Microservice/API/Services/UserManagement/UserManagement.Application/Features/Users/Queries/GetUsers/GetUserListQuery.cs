using MediatR;
using System;
using System.Collections.Generic;


namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetUserListQuery : IRequest<List<UsersVm>>
    {
        public GetUserListQuery()
        {

        }
    }
}
