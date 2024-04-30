using MediatR;
using System.Collections.Generic;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey
{
    public class GetAssessmentSurveyByIdQuery : IRequest<List<AssessmentSurveysVm>>
    {
        public long Id { get; set; }

        public GetAssessmentSurveyByIdQuery(long id)
        {
            Id = id;
        }
    }
}
