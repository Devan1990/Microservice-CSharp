﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Role.Queries.GetRoles
{
    public class GetExpectedLevelListQuery : IRequest<List<ExpectedLevelVm>>
    {
        public GetExpectedLevelListQuery()
        {

        }
    }
}
