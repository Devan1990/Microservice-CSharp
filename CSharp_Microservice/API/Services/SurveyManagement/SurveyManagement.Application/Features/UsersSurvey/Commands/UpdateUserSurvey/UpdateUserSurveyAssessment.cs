using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Commands.UpdateUserSurvey
{
    public class UpdateUserSurveyAssessment
    {
        public long Id { get; set; }
        public long AssessorId { get; set; }
        public long AssessmentTypeId { get; set; }
    }
}
