﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetCountryListQuery : IRequest<List<CountryVm>>
    {
        public GetCountryListQuery()
        {

        }
    }
}
