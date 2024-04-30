namespace CAT.BFF.Models
{
    public class AssessmentSurveysRespVm
    {
        public long Id { get; set; }
        public string AssessmentSurveyId { get; set; }
        public long UserSurveyAssessmentId{ get; set; }
        public long UserId { get; set; }
        //public string EmployeeName { get; set; }
        public long RoleId { get; set; }
        public long CompetencyGroupId { get; set; }
        public long CompetencyId { get; set; }
        public long BenchMarkId { get; set; }
        public long ActualLevelId { get; set; }
    }
}
