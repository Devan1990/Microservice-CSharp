using AutoMapper;
using MediatR;
using SurveyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey
{
    public class GetAssessmentSurveyByIdQueryHandler : IRequestHandler<GetAssessmentSurveyByIdQuery, List<AssessmentSurveysVm>>
    {
        private readonly IAssessmentSurveyRepository _assessmentsurveyrepository;
        private readonly IMapper _mapper;
        public GetAssessmentSurveyByIdQueryHandler(IAssessmentSurveyRepository assessmentsurveyrepository, IMapper mapper)
        {
            _assessmentsurveyrepository = assessmentsurveyrepository ?? throw new ArgumentNullException(nameof(assessmentsurveyrepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<AssessmentSurveysVm>> Handle(GetAssessmentSurveyByIdQuery request, CancellationToken cancellationToken)
        {
            var assessmentsurvey = await _assessmentsurveyrepository.GetAssessmentSurveyByusersurveyId(request.Id);
            return _mapper.Map<List<AssessmentSurveysVm>>(assessmentsurvey);
        }
    }
}
