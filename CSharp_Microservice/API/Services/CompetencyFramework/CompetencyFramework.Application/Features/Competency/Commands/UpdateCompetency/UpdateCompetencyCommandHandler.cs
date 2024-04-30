using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency
{
    public class UpdateCompetencyCommandHandler : IRequestHandler<UpdateCompetencyCommand>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ICompetencyRepository _competencyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompetencyCommandHandler> _logger;

        public UpdateCompetencyCommandHandler(IAttributeRepository attributeRepository, ICompetencyRepository competencyRepository, IMapper mapper, ILogger<UpdateCompetencyCommandHandler> logger)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _competencyRepository = competencyRepository ?? throw new ArgumentNullException(nameof(competencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateCompetencyCommand request, CancellationToken cancellationToken)
        {
            var competencyToUpdate = await _competencyRepository.GetByIdAsync(request.Id);
            if (competencyToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Competency), request.Id);
            }


            foreach (var req_att in request.Attributes)
            {
                var attributeToUpdate = await _attributeRepository.GetByIdAsync(req_att.Id);
                if (attributeToUpdate == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Attribute), req_att.Id);
                }
                foreach (var cmp_att in competencyToUpdate.Attributes)
                {
                    cmp_att.Description = req_att.Description;
                    cmp_att.CreatedBy = attributeToUpdate.CreatedBy;
                    cmp_att.CreatedDate = attributeToUpdate.CreatedDate;
                }
                _attributeRepository.DisposeAttribute(attributeToUpdate);
            }

            _mapper.Map(request, competencyToUpdate, typeof(UpdateCompetencyCommand), typeof(Domain.Entities.Competency));

            await _competencyRepository.UpdateAsync(competencyToUpdate);

            _logger.LogInformation($"Competency {competencyToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
