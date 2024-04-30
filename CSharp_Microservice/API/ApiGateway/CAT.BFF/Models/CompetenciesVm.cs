namespace CATBFF.Models
{
    public class CompetenciesVm
    {
        public int Id { get; set; }
        public string CompetencyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributesVm> Attributes { get; set; }
    }
}
