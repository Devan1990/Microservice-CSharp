using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class UserSurveyAssessmentVm
    {
        public long Id { get; set; }
        public long AssessorId { get; set; }
        public long AssessorRoleId { get; set; }
        public bool AssessorStatus { get; set; }
        public UserSurveyAssessmentTypeVM AssessmentTypeId { get; set; }
    }
}
