using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute
{
    public class GetAttributeByIdQueryHandler : IRequestHandler<GetAttributeByIdQuery, AttributesVm>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public GetAttributeByIdQueryHandler(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AttributesVm> Handle(GetAttributeByIdQuery request, CancellationToken cancellationToken)
        {
            var attributeList = await _attributeRepository.GetAttributeById(request.Id);
            return _mapper.Map<AttributesVm>(attributeList);
        }
    }
}
