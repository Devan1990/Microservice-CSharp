using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Competency.Queries.GetCompetency
{
    public class GetCompetencyListQueryHandler : IRequestHandler<GetCompetencyListQuery, List<CompetenciesVm>>
    {
        private readonly ICompetencyRepository _competencyRepository;
        private readonly IMapper _mapper;

        public GetCompetencyListQueryHandler(ICompetencyRepository competencyRepository, IMapper mapper)
        {
            _competencyRepository = competencyRepository ?? throw new ArgumentNullException(nameof(competencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CompetenciesVm>> Handle(GetCompetencyListQuery request, CancellationToken cancellationToken)
        {
            var competencyList = await _competencyRepository.GetCompetencies();
            return _mapper.Map<List<CompetenciesVm>>(competencyList);
        }
    }
}
