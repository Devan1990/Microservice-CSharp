using SurveyManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Domain.Entities
{
    public class UserSurveyAssessment : EntityBase
    {
       
        public UserSurvey UserSurvey { get; set; }
        public long AssessorId { get; set; }
        public long AssessorRoleId { get; set; }
        //public bool AssessorStatus { get; set; }
        public AssessmentType AssessmentType { get; set; }

    }
}
