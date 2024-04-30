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
    public class GetAreaListQueryHandler: IRequestHandler<GetAreaListQuery, List<AreaVm >>
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;

        public GetAreaListQueryHandler(IAreaRepository areaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<AreaVm>> Handle(GetAreaListQuery request, CancellationToken cancellationToken)
        {
            var arealist = await _areaRepository.GetAreas();
            return _mapper.Map<List<AreaVm>>(arealist);
        }
    }
}
