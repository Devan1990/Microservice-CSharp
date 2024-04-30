using MediatR;
using System;
using System.Collections.Generic;


namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetUserByIdQuery : IRequest<UsersVm>
    {
        public long Id { get; set; }

        public GetUserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
