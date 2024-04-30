using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Application.Features.Users.Queries.GetUsers
{
    public class GetCountryListQueryHandler: IRequestHandler<GetCountryListQuery, List<CountryVm >>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryListQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CountryVm>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var countryList = await _countryRepository.GetCountries();
            return _mapper.Map<List<CountryVm>>(countryList);
        }
    }
}
