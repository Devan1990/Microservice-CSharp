using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class UserSurveyAssessmentSurveyVm
    {
        public long Id { get; set; }
        public UserSurveyVm UserSurveyId { get; set; }
        //public long AssessorId { get; set; }
        //public long AssessorRoleId { get; set; }
        //public bool AssessorStatus { get; set; }
        //public UserSurveyAssessmentTypeVM AssessmentTypeId { get; set; }
    }
}
