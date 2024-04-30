namespace CAT.BFF.Models
{
    public class CompetenciesUserSurveyVm
    {
        public int Id { get; set; }
        public string CompetencyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long BenchMarkId { get; set; }
        public string BenchMark { get; set; }
        public long ActualId { get; set; }
        public string ActualBenchMark { get; set; }
    }
}
