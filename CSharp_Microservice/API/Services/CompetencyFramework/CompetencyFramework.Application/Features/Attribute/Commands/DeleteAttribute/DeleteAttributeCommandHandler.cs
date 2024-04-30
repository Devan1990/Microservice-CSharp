using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Commands.DeleteAttribute
{
    public class DeleteAttributeCommandHandler : IRequestHandler<DeleteAttributeCommand>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAttributeCommandHandler> _logger;

        public DeleteAttributeCommandHandler(IAttributeRepository attributeRepository, IMapper mapper, ILogger<DeleteAttributeCommandHandler> logger)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
        {
            var attributeToDelete = await _attributeRepository.GetByIdAsync(request.Id);
            if (attributeToDelete == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Attribute), request.Id);
            }            

            await _attributeRepository.DeleteAsync(attributeToDelete);

            _logger.LogInformation($"Competency {attributeToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
