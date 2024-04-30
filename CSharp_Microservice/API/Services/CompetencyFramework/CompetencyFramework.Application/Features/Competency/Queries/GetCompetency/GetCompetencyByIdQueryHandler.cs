using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Competency.Queries.GetCompetency
{
    public class GetCompetencyByIdQueryHandler : IRequestHandler<GetCompetencyByIdQuery, CompetenciesVm>
    {
        private readonly ICompetencyRepository _competencyRepository;
        private readonly IMapper _mapper;

        public GetCompetencyByIdQueryHandler(ICompetencyRepository competencyRepository, IMapper mapper)
        {
            _competencyRepository = competencyRepository ?? throw new ArgumentNullException(nameof(competencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CompetenciesVm> Handle(GetCompetencyByIdQuery request, CancellationToken cancellationToken)
        {
            var competencyList = await _competencyRepository.GetCompetencyById(request.Id);
            return _mapper.Map<CompetenciesVm>(competencyList);
        }
    }
}
