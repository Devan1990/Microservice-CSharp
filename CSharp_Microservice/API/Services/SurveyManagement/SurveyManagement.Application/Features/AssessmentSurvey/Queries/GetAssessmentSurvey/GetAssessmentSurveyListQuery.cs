using MediatR;
using System.Collections.Generic;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey
{
    public class GetAssessmentSurveyListQuery : IRequest<List<AssessmentSurveysVm>>
    {
        public GetAssessmentSurveyListQuery()
        {

        }
    }
}
