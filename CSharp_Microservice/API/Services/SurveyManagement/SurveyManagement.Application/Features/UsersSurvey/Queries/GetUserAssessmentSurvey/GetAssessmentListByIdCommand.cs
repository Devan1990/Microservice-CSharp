using MediatR;
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetAssessmentSurvey
{
    public class GetAssessmentListByIdCommand : IRequest<List<UserSurveyVm>>
    {
        public ICollection<AssessmentUserSurveyVm> usersurveyvm { get; set; }
    }
}
