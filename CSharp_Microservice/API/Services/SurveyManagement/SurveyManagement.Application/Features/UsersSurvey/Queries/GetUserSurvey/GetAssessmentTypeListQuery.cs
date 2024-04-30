using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class GetAssessmentTypeListQuery : IRequest<List<AssessmentTypeVM>>
    {
        public GetAssessmentTypeListQuery()
        {

        }
    }
}
