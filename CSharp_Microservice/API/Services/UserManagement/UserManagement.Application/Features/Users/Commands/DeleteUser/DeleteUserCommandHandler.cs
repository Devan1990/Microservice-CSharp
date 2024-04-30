using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Features.Users.Commands.DeleteUser;
using UserManagement.Domain.Entities;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetByIdAsync(request.Id);
            if (userToDelete == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            userToDelete.IsDeleted = true;
            await _userRepository.UpdateAsync(userToDelete);

            _logger.LogInformation($"user {userToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
