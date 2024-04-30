using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class UserSurveyVm2
    {
        public long ID { get; set; }
        public long AssesseeUId { get; set; }
        public string AssesseeUserId { get; set; }
        public string AssessmentID { get; set; }
        public long SurveyID { get; set; }
        public string SurveyName { get; set; }
        public string AssesseeName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string AssesseeRoleName { get; set; }
        public long AssesseeRoleId { get; set; }
        public long AssessorRoleId { get; set; }
        public long AssessorUId { get; set; }
        public string AssessorUserId { get; set; }
        public string AssessorUserName { get; set; }
        public string AssessorRoleName { get; set; }
        public bool AssessorStatus { get; set; }
        public long assessmentTypeId { get; set; }
        public string AssessmentStatus { get; set; }
    }
}
