using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Application.Features.RoleMapping.Queries.GetRoleMapping
{
    public class GetRoleMappingLIstQueryHandler : IRequestHandler<GetRoleMappingListQuery, List<RoleMappingVm>>
    {
        private readonly IRoleMappingRepository _rolemappingrepository;
        private readonly IRoleRepository _rolerepository;
        private readonly IMapper _mapper;

        public GetRoleMappingLIstQueryHandler(IRoleMappingRepository roleMappingRepository, IMapper mapper)
        {
            _rolemappingrepository = roleMappingRepository ?? throw new ArgumentNullException(nameof(roleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<RoleMappingVm>> Handle(GetRoleMappingListQuery request, CancellationToken cancellationToken)
        {
            var roleList = await _rolemappingrepository.GetRoleMappings();
            //foreach(var item in roleList){
            //    if(item!=null && item.RoleId != null){
            //     var role= await _rolerepository.GetRoleByRoleId(item.RoleId);
            //    item.RoleType = role.RoleType;
            //    }
                
            //}

            return _mapper.Map<List<RoleMappingVm>>(roleList);
        }
    }
}
