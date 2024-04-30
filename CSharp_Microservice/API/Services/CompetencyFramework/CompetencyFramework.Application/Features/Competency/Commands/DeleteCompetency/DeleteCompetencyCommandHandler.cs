using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Competency.Commands.DeleteCompetency
{
    public class DeleteCompetencyCommandHandler : IRequestHandler<DeleteCompetencyCommand>
    {
        private readonly ICompetencyRepository _competencyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCompetencyCommandHandler> _logger;

        public DeleteCompetencyCommandHandler(ICompetencyRepository competencyRepository, IMapper mapper, ILogger<DeleteCompetencyCommandHandler> logger)
        {
            _competencyRepository = competencyRepository ?? throw new ArgumentNullException(nameof(competencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteCompetencyCommand request, CancellationToken cancellationToken)
        {
            var competencyToDelete = await _competencyRepository.GetByIdAsync(request.Id);
            if (competencyToDelete == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Competency), request.Id);
            }            

            await _competencyRepository.DeleteAsync(competencyToDelete);

            _logger.LogInformation($"Competency {competencyToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
