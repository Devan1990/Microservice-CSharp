using AutoMapper;
using MediatR;
using SurveyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Queries.GetSurvey
{
    public class GetSurveyByIdQueryHandler : IRequestHandler<GetSurveyByIdQuery, SurveyVm>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public GetSurveyByIdQueryHandler(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SurveyVm> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
        {
            var surveyList = await _surveyRepository.GetSurveyById(request.Id);
            return _mapper.Map<SurveyVm>(surveyList);
        }

    }
}
