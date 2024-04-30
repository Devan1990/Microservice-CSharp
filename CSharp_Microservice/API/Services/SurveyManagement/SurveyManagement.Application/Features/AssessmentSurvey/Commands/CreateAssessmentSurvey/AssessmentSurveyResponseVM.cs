using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Commands.CreateAssessmentSurvey
{
    public class AssessmentSurveyResponseVM
    {
        public long UserSurveyAssessmentId { get; set; }
        public long AssessmentTypeId { get; set; }
        public string Remarks { get; set; }
    }
}
