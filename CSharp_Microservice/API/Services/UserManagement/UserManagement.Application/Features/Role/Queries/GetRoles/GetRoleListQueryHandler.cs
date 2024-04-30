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
    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, List<RoleVm2>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleListQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<RoleVm2>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roleList = await _roleRepository.GetRoles();
            return _mapper.Map<List<RoleVm2>>(roleList);
        }
    }
}
