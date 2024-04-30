using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Queries.GetSurvey
{
    public class SurveyVm
    {
        //public Survey()
        //{
        //    SurveyRoleMappings = new List<SurveyRoleMapping>();
        //}
        public long Id { get; set; }
        public string SurveyId { get; set; }
        public long RoleId { get; set; }
        public string RoleAID { get; set; }

        public string RoleName { get; set; }
        public DateTime FromPeriod { get; set; }
        public DateTime ToPeriod { get; set; }
        public DateTime CreatedDate { get; set; }
       // public ICollection<SurveyRoleMappingVm> SurveyRoleMappings { get; set; }
    }
}
