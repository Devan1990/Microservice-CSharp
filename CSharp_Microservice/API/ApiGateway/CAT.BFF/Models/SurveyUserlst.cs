namespace CAT.BFF.Models
{
    public class SurveyUserlst
    {
        public long usersurveyid { get; set; }
        public long id { get; set; }
        public string userId { get; set; }
        public string employeeName { get; set; }
        public string roleName { get; set; }
        public long benchMarkId { get; set; }
        public string benchMark { get; set; }
        public string roleId { get; set; }
        public long roleTId { get; set; }
        public CompetenciesMapList competenciesMapList { get; set; }
    }
}
