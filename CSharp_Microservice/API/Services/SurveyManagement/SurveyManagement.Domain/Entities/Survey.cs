using SurveyManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SurveyManagement.Domain.Entities
{
    public class Survey : EntityBase
    {
        //public Survey()
        //{
        //    SurveyRoleMappings = new List<SurveyRoleMapping>();
        //}
        public string SurveyId { get; set; }
        public long RoleId { get; set; }

        public DateTime FromPeriod { get; set; }
        public DateTime ToPeriod { get; set; }
        public bool IsDeleted { get; set; } = false;
      //  public ICollection<SurveyRoleMapping> SurveyRoleMappings { get; set; }
    }
}
