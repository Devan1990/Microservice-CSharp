using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetRegionListQueryHandler : IRequestHandler<GetRegionListQuery, List<RegionVm >>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public GetRegionListQueryHandler(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<RegionVm>> Handle(GetRegionListQuery request, CancellationToken cancellationToken)
        {
            var regionlist = await _regionRepository.GetRegions();
            return _mapper.Map<List<RegionVm>>(regionlist);
        }
    }
}
