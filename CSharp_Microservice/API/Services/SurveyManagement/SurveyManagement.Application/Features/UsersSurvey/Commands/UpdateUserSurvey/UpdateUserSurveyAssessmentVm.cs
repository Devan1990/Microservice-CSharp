using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Commands.UpdateUserSurvey
{
    public class UpdateUserSurveyAssessmentVm
    {
        public long Id { get; set; }
        public long AssessorUserId { get; set; }
        public AssessmenttypeVm AssessmentTypeId { get; set; }
    }
}
