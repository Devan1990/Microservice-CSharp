using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.DeleteCompetencyGroup
{
    public class DeleteCompetencyGroupCommandHandler : IRequestHandler<DeleteCompetencyGroupCommand>
    {
        private readonly ICompetencyGroupRepository _competencyGroupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCompetencyGroupCommandHandler> _logger;

        public DeleteCompetencyGroupCommandHandler(ICompetencyGroupRepository competencyGroupRepository, IMapper mapper, ILogger<DeleteCompetencyGroupCommandHandler> logger)
        {
            _competencyGroupRepository = competencyGroupRepository ?? throw new ArgumentNullException(nameof(competencyGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteCompetencyGroupCommand request, CancellationToken cancellationToken)
        {
            var competencyGroupToDelete = await _competencyGroupRepository.GetByIdAsync(request.Id);
            if (competencyGroupToDelete == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.CompetencyGroup), request.Id);
            }            

            await _competencyGroupRepository.DeleteAsync(competencyGroupToDelete);

            _logger.LogInformation($"Competency Group {competencyGroupToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
