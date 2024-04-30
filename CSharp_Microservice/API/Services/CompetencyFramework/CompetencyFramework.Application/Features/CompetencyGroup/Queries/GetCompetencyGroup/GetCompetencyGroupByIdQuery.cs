using MediatR;
using System;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup
{
    public class GetCompetencyGroupByIdQuery : IRequest<CompetencyGroupsVm>
    {
        public long Id { get; set; }

        public GetCompetencyGroupByIdQuery(long id)
        {
            Id = id;
        }
    }
}
