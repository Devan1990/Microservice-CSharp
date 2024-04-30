using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UsersVm>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UsersVm>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetUsers();
            return _mapper.Map<List<UsersVm>>(userList);
        }
    }
}
