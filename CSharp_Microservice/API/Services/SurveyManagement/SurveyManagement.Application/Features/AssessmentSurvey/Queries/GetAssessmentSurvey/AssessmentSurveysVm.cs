using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Queries.GetAssessmentSurvey
{
    public class AssessmentSurveysVm
    {
        public long Id { get; set; }
        public string AssessmentSurveyId { get; set; }
        public long UserSurveyAssessmentId { get; set; }
        public long UserId { get; set; }
        //public string EmployeeName { get; set; }
        public long RoleId { get; set; }
        public long CompetencyGroupId { get; set; }
        public long CompetencyId { get; set; }
        public long BenchMarkId { get; set; }
        public long ActualLevelId { get; set; }
    }
}
