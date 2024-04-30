namespace CAT.BFF.Models
{
    public class GetAssessmentListByIdCommand
    {
        public ICollection<AssessmentSurveyVm> AssessmentSurvey { get; set; }
    }
}
