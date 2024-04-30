using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup
{
    public class GetCompetencyGroupByIdQueryHandler : IRequestHandler<GetCompetencyGroupByIdQuery, CompetencyGroupsVm>
    {
        private readonly ICompetencyGroupRepository _competencyGroupRepository;
        private readonly IMapper _mapper;

        public GetCompetencyGroupByIdQueryHandler(ICompetencyGroupRepository competencyGroupRepository, IMapper mapper)
        {
            _competencyGroupRepository = competencyGroupRepository ?? throw new ArgumentNullException(nameof(competencyGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CompetencyGroupsVm> Handle(GetCompetencyGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var competencyGroupList = await _competencyGroupRepository.GetCompetencyGroupById(request.Id);
            return _mapper.Map<CompetencyGroupsVm>(competencyGroupList);
        }
    }
}
