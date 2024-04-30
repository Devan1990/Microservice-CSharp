using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;

namespace UserManagement.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<DeleteRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            var RoleToDelete = await _roleRepository.GetByIdAsync(request.Id);
            if (RoleToDelete == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Role), request.Id);
            }
            RoleToDelete.IsDeleted = true;
            await _roleRepository.UpdateAsync(RoleToDelete);

            _logger.LogInformation($"Role  {RoleToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
