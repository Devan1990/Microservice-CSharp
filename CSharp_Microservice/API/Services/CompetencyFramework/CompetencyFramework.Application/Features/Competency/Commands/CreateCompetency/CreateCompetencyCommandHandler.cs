using AutoMapper;
using CompetencyFramework.Application.Contracts.Infrastructure;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Application.Models;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency
{
    public class CreateCompetencyCommandHandler : IRequestHandler<CreateCompetencyCommand, long>
    {
        private readonly ICompetencyLevelRepository _competencyLevelRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ICompetencyGroupRepository _competencyGroupRepository;
        private readonly ICompetencyRepository _competencyRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateCompetencyCommandHandler> _logger;

        public CreateCompetencyCommandHandler(ICompetencyLevelRepository competencyLevelRepository, IAttributeRepository attributeRepository, ICompetencyGroupRepository competencyGroupRepository, ICompetencyRepository competencyRepository, IMapper mapper, IEmailService emailService, ILogger<CreateCompetencyCommandHandler> logger)
        {
            _competencyLevelRepository = competencyLevelRepository ?? throw new ArgumentNullException(nameof(competencyLevelRepository));
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _competencyGroupRepository = competencyGroupRepository ?? throw new ArgumentNullException(nameof(competencyGroupRepository));
            _competencyRepository = competencyRepository ?? throw new ArgumentNullException(nameof(competencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<long> Handle(CreateCompetencyCommand request, CancellationToken cancellationToken)
        {
            var group = await _competencyGroupRepository.GetCompetencyGroupById(request.CompetencyGroupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.CompetencyGroup), request.CompetencyGroupId);
            }

            var competency = _mapper.Map<Domain.Entities.Competency>(request);

            foreach (var req_att in request.Attributes)
            {
                var competencyLevel = await _competencyLevelRepository.GetCompetencyLevelById(req_att.CompetencyLevelId);
                if (competencyLevel == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.CompetencyLevel), req_att.CompetencyLevelId);
                }
                foreach (var cmp_att in competency.Attributes)
                {
                    cmp_att.CompetencyLevel = competencyLevel;

                    var AT = await _attributeRepository.GetAttributeOrderById(null, b => b.OrderByDescending(b => b.Id));
                    var AT_ID = AT == null ? 1 : AT.Id + 1;

                }
            }

            group.Status = ActiveStatus.Draft;
            competency.CompetencyGroup = group;

            var CM = await _competencyRepository.GetCompetencyOrderById(null, b => b.OrderByDescending(b => b.Id));
            var CM_ID = CM == null ? 1 : CM.Id + 1;
            competency.CompetencyId = "CM" + CM_ID.ToString().PadLeft(CM_ID.ToString().Length + 5 - CM_ID.ToString().Length, '0');

            var entity = await _competencyRepository.AddAsync(competency);

            _logger.LogInformation($"Competency {entity.Id} is successfully created.");

            return entity.Id;
        }

        private async Task SendMail(Domain.Entities.Competency competency)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Competency was created.", Subject = "Competency was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Competency {competency.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
