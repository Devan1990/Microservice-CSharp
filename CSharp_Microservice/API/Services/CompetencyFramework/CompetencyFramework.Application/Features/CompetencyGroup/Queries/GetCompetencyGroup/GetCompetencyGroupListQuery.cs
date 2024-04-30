using MediatR;
using System;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup
{
    public class GetCompetencyGroupListQuery : IRequest<List<CompetencyGroupsVm>>
    {
        public GetCompetencyGroupListQuery()
        {
        }
    }
}
