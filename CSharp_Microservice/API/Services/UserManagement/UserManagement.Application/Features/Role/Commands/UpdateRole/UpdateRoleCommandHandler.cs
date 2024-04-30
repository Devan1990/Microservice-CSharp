using AutoMapper;
using CompetencyFramework.API.GrpcServices;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand,long>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly CompetencyFrameworkGrpcService _competencyFrameworkGrpcService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, CompetencyFrameworkGrpcService competencyFrameworkGrpcService, IMapper mapper, ILogger<UpdateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _competencyFrameworkGrpcService = competencyFrameworkGrpcService ?? throw new ArgumentNullException(nameof(competencyFrameworkGrpcService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<long> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            
            foreach (var map in request.CompetenciesMap)
            {
                var exists = await _competencyFrameworkGrpcService.IsCompetencyExists(map.CompetencyId);
                if (!exists.Exists)
                {
                    throw new NotFoundException("Competency", map.CompetencyId);
                }
            }
            foreach (var map in request.CompetenciesMap)
            {
                var exists = await _competencyFrameworkGrpcService.IsCompetencyGroupExists(map.CompetencyGroupId);
                if (!exists.Exists)
                {
                    throw new NotFoundException("Competency", map.CompetencyGroupId);
                }
            }
            var roleToUpdate = await _roleRepository.GetRoleById(request.Id);
            if (roleToUpdate == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var existingMap = roleToUpdate.CompetenciesMap.ToList();

            var updatedMap = roleToUpdate.CompetenciesMap.Select(a =>
            {
                a.IsSelected = false;
                a.IsDeleted = true;
                return a;
            }).ToList();

            foreach (var mapEntry in request.CompetenciesMap)
            {
                var map = updatedMap.FirstOrDefault(a => a.Id == mapEntry.Id);

                if (map == null)
                {
                    updatedMap.Add(_mapper.Map<CompetenciesMap>(mapEntry));
                }
                else
                {
                    map.CompetencyGroupId = mapEntry.CompetencyGroupId;
                    map.CompetencyId = mapEntry.CompetencyId;
                    map.IsSelected = mapEntry.IsSelected;
                    map.ExpectedLevelId = mapEntry.ExpectedLevelId;
                    map.IsDeleted = mapEntry.IsDeleted;
                }
            }
            _mapper.Map(request, roleToUpdate, typeof(UpdateRoleCommand), typeof(Domain.Entities.Role));

            roleToUpdate.CompetenciesMap = updatedMap;
           var entity= await _roleRepository.UpdateRole(roleToUpdate);

            _logger.LogInformation($"Role {roleToUpdate.Id} is successfully updated.");

            return entity;
        }


    }
}
