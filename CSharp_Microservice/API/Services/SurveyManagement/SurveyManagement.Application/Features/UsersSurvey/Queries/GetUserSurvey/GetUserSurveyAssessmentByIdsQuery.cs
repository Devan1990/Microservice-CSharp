using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class GetUserSurveyAssessmentByIdsQuery:IRequest<List<UserSurveyAssessmentSurveyVm>>
    {
        public ICollection<AssessmentUserSurveyVm> UserSurvey { get; set; }
    }
}
