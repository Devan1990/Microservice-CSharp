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
    public class GetVerticalsListQueryHandler: IRequestHandler<GetVerticalsListQuery, List<VerticalVm>>
    {
        private readonly IverticalRepository _verticalRepository;
        private readonly IMapper _mapper;

        public GetVerticalsListQueryHandler(IverticalRepository verticalRepository, IMapper mapper)
        {
            _verticalRepository = verticalRepository ?? throw new ArgumentNullException(nameof(verticalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<VerticalVm>> Handle(GetVerticalsListQuery request, CancellationToken cancellationToken)
        {
            var VerticalsList = await _verticalRepository.GetVerticals();
            return _mapper.Map<List<VerticalVm>>(VerticalsList);
        }
    }
}
