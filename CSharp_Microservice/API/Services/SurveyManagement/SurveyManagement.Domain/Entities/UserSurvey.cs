using SurveyManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Domain.Entities
{
    public class UserSurvey : EntityBase
    {
        public UserSurvey()
        {
            UserSurveyAssessments = new List<UserSurveyAssessment>();
        }
        public string AssessmentID { get; set; }
        public long SurveyId { get; set; }
        public long UserId { get; set; }
        public string AssessmentStatus { get; set; }
        public ICollection<UserSurveyAssessment> UserSurveyAssessments { get; set; }
    }
}
