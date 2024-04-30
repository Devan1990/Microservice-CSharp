using UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UserManagement.Application.Contracts.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using System.Collections.Generic;

namespace UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping
{
    public class CreateRoleMappingCommandHandler : IRequestHandler<CreateRoleMappingCommand, long>
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateRoleMappingCommandHandler> _logger;

        public CreateRoleMappingCommandHandler(IRoleRepository roleRepository, IRoleMappingRepository roleMappingRepository, IMapper mapper, IEmailService emailService, ILogger<CreateRoleMappingCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _roleMappingRepository = roleMappingRepository ?? throw new ArgumentException(nameof(roleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<long> Handle(CreateRoleMappingCommand request, CancellationToken cancellationToken)
        {
            var roleMapping1 = (await _roleMappingRepository.GetAsync(a => ((a.AssessmentPeriodFrom.Year == request.AssessmentPeriodFrom.Year) && (a.AssessmentPeriodTo.Year == request.AssessmentPeriodTo.Year)))).FirstOrDefault();
            var roleMappingQuery = await _roleMappingRepository.GetRoleMappingQuery(a => a.RoleId == request.RoleId && a.roleMapping.AssessmentPeriodFrom.Year == request.AssessmentPeriodFrom.Year && a.roleMapping.AssessmentPeriodTo.Year == request.AssessmentPeriodTo.Year);
            
            
             if ((request.AssessmentPeriodTo.Year - request.AssessmentPeriodFrom.Year) > 1)
            {
                throw new Exception("FY difference should not be greater than 1 year");
            }

            if (roleMappingQuery.Count > 0)
            {
                throw new Exception("SurveyID already exists for given Role");
            }

            foreach (var map in request.AssessorRole)
            {
                var role1 = await _roleRepository.CheckRole(map.RoleId);
                if (role1 == null)
                {
                    throw new NotFoundException("Role", map.RoleId);
                }
            }
            
            var role = await _roleRepository.CheckRole(request.RoleId);
            if (role == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Role), request.RoleId);
            }
            var roleMapping = _mapper.Map<Domain.Entities.RoleMapping>(request);
            var assessorrole = new AssessorRole() { RoleId = request.RoleId };
            assessorrole.roleMapping = roleMapping1;
            var entity = await _roleMappingRepository.AddAsync(roleMapping);
            if (roleMapping == null)
            {
                throw new BadRequestException();
            }
             _logger.LogInformation($"RoleMapping  {entity.Id} is successfully created.");
           // var entity = await _roleMappingRepository.AddAsync(roleMapping);
            return entity.Id;
        }
    }

}



