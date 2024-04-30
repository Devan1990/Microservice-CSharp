namespace CAT.BFF.Models
{
    public class CompetenciesMap
    {
        public long competencyGroupId { get; set; }
        public long competencyId { get; set; }
        public bool isSelected { get; set; }
        public long expectedLevelId { get; set; }
    }
}
