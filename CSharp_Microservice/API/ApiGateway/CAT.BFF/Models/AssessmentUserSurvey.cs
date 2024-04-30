using CATBFF.Models;

namespace CAT.BFF.Models
{
    public class AssessmentUserSurvey
    {
        public long usersurveyid { get; set; }
        public long id { get; set; }
        public string userId { get; set; }
        public string employeeName { get; set; }
        public string roleName { get; set; }
        public string roleId { get; set; }
        public long roleTId { get; set; }
        public List<CompetencyGroupsUserSurveyVm> CompetenciesMap { get; set; }
        
    }
}
