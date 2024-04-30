using AutoMapper;
using MediatR;
using SurveyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey
{
    public class GetAssessmentSurveyListQueryHandler : IRequestHandler<GetAssessmentSurveyListQuery, List<AssessmentSurveysVm>>
    {
        private readonly IAssessmentSurveyRepository _assessmentsurveyrepository;
        private readonly IMapper _mapper;
        public GetAssessmentSurveyListQueryHandler(IAssessmentSurveyRepository assessmentsurveyrepository, IMapper mapper)
        {
            _assessmentsurveyrepository = assessmentsurveyrepository ?? throw new ArgumentNullException(nameof(assessmentsurveyrepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<AssessmentSurveysVm>> Handle(GetAssessmentSurveyListQuery request, CancellationToken cancellationToken)
        {
            var assessmentsurvey = await _assessmentsurveyrepository.GetSurvey();
            return _mapper.Map<List<AssessmentSurveysVm>>(assessmentsurvey);
        }
    }
}
