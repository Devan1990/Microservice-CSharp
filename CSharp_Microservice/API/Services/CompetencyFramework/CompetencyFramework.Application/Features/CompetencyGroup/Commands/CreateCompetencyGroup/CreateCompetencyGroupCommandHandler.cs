using AutoMapper;
using CompetencyFramework.Application.Contracts.Infrastructure;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Application.Models;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup
{
    public class CreateCompetencyGroupCommandHandler : IRequestHandler<CreateCompetencyGroupCommand, long>
    {
        private readonly ICompetencyGroupRepository _competencyGroupRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateCompetencyGroupCommandHandler> _logger;
        private readonly ICompetencyLevelRepository _competencyLevelRepository;

        public CreateCompetencyGroupCommandHandler(ICompetencyGroupRepository competencyGroupRepository, IMapper mapper, IEmailService emailService, ILogger<CreateCompetencyGroupCommandHandler> logger, ICompetencyLevelRepository competencyLevelRepository)
        {
            _competencyGroupRepository = competencyGroupRepository ?? throw new ArgumentNullException(nameof(competencyGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _competencyLevelRepository = competencyLevelRepository ?? throw new ArgumentNullException(nameof(competencyLevelRepository));
        }

        public async Task<long> Handle(CreateCompetencyGroupCommand request, CancellationToken cancellationToken)
        {
            
            var competencyGroup = _mapper.Map<Domain.Entities.CompetencyGroup>(request);
            if (competencyGroup == null)
            {
                throw new BadRequestException();
            }

            var competency = _mapper.Map<ICollection<Domain.Entities.Competency>>(request.Competencies);
            var cg = request.Competencies.ToList();
            for (int i=0;i< cg.Count;i++)
            {
                var att = cg[i].Attributes.ToList();
                for (int a = 0; a < att.Count; a++)
                {
                    var competencyLevel = await _competencyLevelRepository.GetCompetencyLevelById(att[a].CompetencyLevelId);
                    if (competencyLevel == null)
                    {
                        throw new NotFoundException(nameof(Domain.Entities.CompetencyLevel), att[a].CompetencyLevelId);
                    }
                    else
                    {
                        competencyGroup.Competencies.ToList()[i].Attributes.ToList()[a].CompetencyLevel = competencyLevel;
                    }
                }
            }
            competencyGroup.Status = ActiveStatus.Draft;
            competencyGroup.Name = request.CompetencyGroupName;
            competencyGroup.Description = request.CompetencyGroupDescription;
            var entity = await _competencyGroupRepository.AddCompetencyGroup(competencyGroup);
            _logger.LogInformation($"Competency Group {entity} is successfully created.");
            // await SendMail(newOrder);
            return entity;
        }

        private async Task SendMail(Domain.Entities.CompetencyGroup order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Competency Group was created.", Subject = "Competency Group was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Competency Group {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
