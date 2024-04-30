using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Commands.CreateAssessmentSurvey
{
    public  class CaptureAssessmentSurveyVm
    {
        public string AssessmentSurveyId { get; set; }
        public long UserSurveyId { get; set; }
        public long UserId { get; set; }
        //public string EmployeeName { get; set; }
        public long RoleId { get; set; }
        public long CompetencyGroupId { get; set; }
        public long CompetencyId { get; set; }
        public long BenchMarkId { get; set; }
       public long ActualLevelId { get; set; }
        public long AssessmentTypeId { get; set; }
    }
}
