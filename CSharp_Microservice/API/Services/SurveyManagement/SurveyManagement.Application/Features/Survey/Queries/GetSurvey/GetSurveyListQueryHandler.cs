using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Features.Survey.Queries.GetSurvey;

namespace SurveyManagement.Application.Features.Survey.Queries.GetSurveys
{
    public class GetSurveyListQueryHandler : IRequestHandler<GetSurveyListQuery, List<SurveyVm>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public GetSurveyListQueryHandler(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<SurveyVm>> Handle(GetSurveyListQuery request, CancellationToken cancellationToken)
        {
            var roleList = await _surveyRepository.GetSurvey();
            return _mapper.Map<List<SurveyVm>>(roleList);
        }
    }
}
