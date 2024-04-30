using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup
{
    public class GetCompetencyGroupListQueryHandler : IRequestHandler<GetCompetencyGroupListQuery, List<CompetencyGroupsVm>>
    {
        private readonly ICompetencyGroupRepository _competencyGroupRepository;
        private readonly IMapper _mapper;

        public GetCompetencyGroupListQueryHandler(ICompetencyGroupRepository competencyGroupRepository, IMapper mapper)
        {
            _competencyGroupRepository = competencyGroupRepository ?? throw new ArgumentNullException(nameof(competencyGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CompetencyGroupsVm>> Handle(GetCompetencyGroupListQuery request, CancellationToken cancellationToken)
        {
            var competencyGroupList = await _competencyGroupRepository.GetCompetencyGroups();
            return _mapper.Map<List<CompetencyGroupsVm>>(competencyGroupList);
        }
    }
}
