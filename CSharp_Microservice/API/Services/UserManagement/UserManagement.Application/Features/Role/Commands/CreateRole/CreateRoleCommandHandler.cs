using AutoMapper;
using CompetencyFramework.API.GrpcServices;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Infrastructure;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, long>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly CompetencyFrameworkGrpcService _competencyFrameworkGrpcService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, CompetencyFrameworkGrpcService competencyFrameworkGrpcService, IMapper mapper, IEmailService emailService, ILogger<CreateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _competencyFrameworkGrpcService = competencyFrameworkGrpcService ?? throw new ArgumentNullException(nameof(competencyFrameworkGrpcService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<long> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            foreach(var map in request.CompetenciesMap)
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
                    throw new NotFoundException("Competency Group", map.CompetencyGroupId);
                }
            }
            foreach (var map in request.CompetenciesMap)
            {
                var expectedLevel = await _roleRepository.CheckExpectedLevel(map.ExpectedLevelId);
                if (expectedLevel == null)
                {
                    throw new NotFoundException("ExpectedLevel", map.ExpectedLevelId);
                }
            }
            var roletype = await _roleRepository.GetRoleTypeById(request.RoleTypeId);
            
            var role = _mapper.Map<Domain.Entities.Role>(request); 
            role.RoleType = roletype;
            if (role == null)
            {
                throw new BadRequestException();
            }
            role.RoleGuid = Guid.NewGuid();
            var entity = await _roleRepository.AddRole(role);

            _logger.LogInformation($"Role  {entity} is successfully created.");

            return entity;


        }

        private async Task SendMail(Domain.Entities.Role order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}

