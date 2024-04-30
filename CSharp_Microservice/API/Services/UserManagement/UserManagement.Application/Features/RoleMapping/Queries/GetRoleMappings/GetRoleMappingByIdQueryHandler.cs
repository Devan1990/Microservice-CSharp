using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping
{
    public class GetRoleMappingByIdQueryHandler : IRequestHandler<GetRoleMappingByIdQuery, RoleMappingVm>
    {
        private readonly IRoleMappingRepository _rolemappingrepository;
        private readonly IMapper _mapper;

        public GetRoleMappingByIdQueryHandler(IRoleMappingRepository roleMappingRepository, IMapper mapper)
        {
            _rolemappingrepository = roleMappingRepository ?? throw new ArgumentNullException(nameof(roleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RoleMappingVm> Handle(GetRoleMappingByIdQuery request, CancellationToken cancellationToken)
        {
            var competencyGroupList = await _rolemappingrepository.GetRoleMappingById(request.Id);
            return _mapper.Map<RoleMappingVm>(competencyGroupList);
        }
    }
}
