using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class AssessmentUserSurvVM
    {
        public long Id { get; set; }
        public string AssessmentID { get; set; }

        public long SurveyId { get; set; }

        public long UserId { get; set; }

        public string AssessmentStatus { get; set; }
    }
}
