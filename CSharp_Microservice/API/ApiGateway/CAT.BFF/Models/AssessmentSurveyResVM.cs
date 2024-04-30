namespace CAT.BFF.Models
{
    public class AssessmentSurveyResVM
    {
		public long usersurveyid { get; set; }
		public long Id { get; set; }
		public string UserId { get; set; }
		public string EmployeeName { get; set; }
		public string RoleName { get; set; }
		public long BenchMarkId { get; set; }
		public string BenchMark { get; set; }
		public string RoleId { get; set; }
		public long RoleTId { get; set; }
		public CompetenciesMapList competenciesMap { get; set; }
	}
}
