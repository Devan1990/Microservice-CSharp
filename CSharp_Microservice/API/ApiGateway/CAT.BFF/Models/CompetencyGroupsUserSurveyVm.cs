namespace CAT.BFF.Models
{
    public class CompetencyGroupsUserSurveyVm
    {
        public int Id { get; set; }
        public string CompetencyGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public ICollection<CompetenciesUserSurveyVm> Competencies { get; set; }
    }
}
