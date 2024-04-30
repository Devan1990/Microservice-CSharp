using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Contracts.Infrastructure;
using UserManagement.Application.Exceptions;
using System.Linq;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.RoleMapping.Commands.UpdateRoleMapping
{
    public class UpdateRoleMappingCommandHandler : IRequestHandler<UpdateRoleMappingCommand>
    {
         private readonly IRoleRepository _roleRepository;
        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateRoleMappingCommandHandler> _logger;

        public UpdateRoleMappingCommandHandler(IRoleMappingRepository roleMappingRepository, IMapper mapper, ILogger<UpdateRoleMappingCommandHandler> logger)
        {
            _roleMappingRepository = roleMappingRepository ?? throw new ArgumentNullException(nameof(roleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var competencyGroupToUpdate = await _roleMappingRepository.GetRoleMappingById(request.Id);
            if (competencyGroupToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.RoleMapping), request.Id);
            }

            var existingAssessor = competencyGroupToUpdate.AssessorRole.ToList();

            var updatedMap = competencyGroupToUpdate.AssessorRole.Select(a =>
            {
                a.IsDeleted = true;
                return a;
            }).ToList();

            foreach (var mapEntry in request.AssessorRole)
            {
                var map = updatedMap.FirstOrDefault(a => a.Id == mapEntry.Id);

                if (map == null)
                {
                    updatedMap.Add(_mapper.Map<AssessorRole>(mapEntry));
                }
                else
                {
                    map.RoleId = mapEntry.RoleId;
                    map.Mandatory = mapEntry.Mandatory;
                    map.IsDeleted = mapEntry.IsDeleted;
                }
            }

            _mapper.Map(request, competencyGroupToUpdate, typeof(UpdateRoleMappingCommand), typeof(Domain.Entities.RoleMapping));
            competencyGroupToUpdate.AssessorRole = updatedMap;
            await _roleMappingRepository.UpdateRoleMapping(competencyGroupToUpdate);

            _logger.LogInformation($"Competency Group {competencyGroupToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
