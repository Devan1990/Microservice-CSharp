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
    public class GetRoleTypeListQueryHandler : IRequestHandler<GetRoleTypeListQuery, List<RoleTypeVm>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleTypeListQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<RoleTypeVm>> Handle(GetRoleTypeListQuery request, CancellationToken cancellationToken)
        {
            var roletypeList = await _roleRepository.GetRoleTypes();
            return _mapper.Map<List<RoleTypeVm>>(roletypeList);
        }
    }
}
