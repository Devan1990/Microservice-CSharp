using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;


namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UsersVm>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UsersVm> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetUserById(request.Id);
            return _mapper.Map<UsersVm>(userList);
        }
    }
}
