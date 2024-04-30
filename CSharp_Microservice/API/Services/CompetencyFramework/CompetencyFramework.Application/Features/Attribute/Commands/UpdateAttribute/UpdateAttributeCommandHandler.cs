using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute
{
    public class UpdateAttributeCommandHandler : IRequestHandler<UpdateAttributeCommand>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAttributeCommandHandler> _logger;

        public UpdateAttributeCommandHandler(IAttributeRepository attributeRepository, IMapper mapper, ILogger<UpdateAttributeCommandHandler> logger)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var attributeToUpdate = await _attributeRepository.GetByIdAsync(request.Id);
            if (attributeToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Attribute), request.Id);
            }

            _mapper.Map(request, attributeToUpdate, typeof(UpdateAttributeCommand), typeof(Domain.Entities.Competency));

            await _attributeRepository.UpdateAsync(attributeToUpdate);

            _logger.LogInformation($"Competency {attributeToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
