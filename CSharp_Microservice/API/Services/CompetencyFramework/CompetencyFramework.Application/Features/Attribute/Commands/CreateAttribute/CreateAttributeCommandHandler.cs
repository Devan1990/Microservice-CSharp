using AutoMapper;
using CompetencyFramework.Application.Contracts.Infrastructure;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute
{
    public class CreateAttributeCommandHandler : IRequestHandler<CreateAttributeCommand, long>
    {
        private readonly ICompetencyLevelRepository _competencyLevelRepository;
        private readonly ICompetencyRepository _competencyRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateAttributeCommandHandler> _logger;

        public CreateAttributeCommandHandler(IAttributeRepository attributeGroupRepository, ICompetencyLevelRepository competencyLevelRepository, ICompetencyRepository competencyRepository, IMapper mapper, IEmailService emailService, ILogger<CreateAttributeCommandHandler> logger)
        {
            _attributeRepository = attributeGroupRepository ?? throw new ArgumentNullException(nameof(attributeGroupRepository));
            _competencyLevelRepository = competencyLevelRepository ?? throw new ArgumentNullException(nameof(competencyLevelRepository));
            _competencyRepository = competencyRepository ?? throw new ArgumentNullException(nameof(competencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<long> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {
            var competency = await _competencyRepository.GetCompetencyById(request.CompetencyId);

            var competencyLevel = await _competencyLevelRepository.GetCompetencyLevelById(request.CompetencyLevelId);
            if (competency == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Competency), request.CompetencyId);
            }
            if (competencyLevel == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.CompetencyLevel), request.CompetencyLevelId);
            }

            var attribute = _mapper.Map<Domain.Entities.Attribute>(request);
            attribute.Competency = competency;
            attribute.CompetencyLevel = competencyLevel;

            var entity = await _attributeRepository.AddAsync(attribute);

            _logger.LogInformation($"Attribute {entity.Id} is successfully created.");

            return entity.Id;
        }

        private async Task SendMail(Domain.Entities.Attribute attribute)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Competency was created.", Subject = "Competency was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Attribute {attribute.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
