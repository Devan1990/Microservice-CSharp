using MediatR;
using System;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.Competency.Queries.GetCompetency
{
    public class GetCompetencyListQuery : IRequest<List<CompetenciesVm>>
    {
        public GetCompetencyListQuery()
        {
        }
    }
}
