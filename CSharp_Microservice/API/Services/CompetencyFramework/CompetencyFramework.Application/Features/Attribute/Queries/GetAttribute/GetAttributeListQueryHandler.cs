using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute
{
    public class GetAttributeListQueryHandler : IRequestHandler<GetAttributeListQuery, List<AttributesVm>>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public GetAttributeListQueryHandler(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository ?? throw new ArgumentNullException(nameof(attributeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<AttributesVm>> Handle(GetAttributeListQuery request, CancellationToken cancellationToken)
        {
            var attributeList = await _attributeRepository.GetAttributes();
            return _mapper.Map<List<AttributesVm>>(attributeList);
        }
    }
}
