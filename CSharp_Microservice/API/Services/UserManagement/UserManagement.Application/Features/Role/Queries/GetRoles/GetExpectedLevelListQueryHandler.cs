using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Application.Features.Role.Queries.GetRoles
{
    public class GetExpectedLevelListQueryHandler:IRequestHandler<GetExpectedLevelListQuery, List<ExpectedLevelVm>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetExpectedLevelListQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<ExpectedLevelVm>> Handle(GetExpectedLevelListQuery request, CancellationToken cancellationToken)
        {
            var roletypeList = await _roleRepository.GetExpectedLevels();
            return _mapper.Map<List<ExpectedLevelVm>>(roletypeList);
        }
    }
}
