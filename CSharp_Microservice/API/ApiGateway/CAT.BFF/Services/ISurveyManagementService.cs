using CAT.BFF.Models;


namespace CAT.BFF.Services
{
    public interface ISurveyManagementService
    {
        //Task<SurveyUserlstVM> GetAssessmentSurvey(GetAssessmentListByIdCommand command);
        Task<List<UserSurveyVm>> GetUserSurvey();

        Task<List<UserSurveyVm>> GetAssessmentSurvey(GetUserSurveyAssessmentByIdsQuery command);


        Task<List<CaptureAssessmentSurveyResponseVM>> CreateAssessmentSurvey(CreateAssessmentSurveyCommand command);

        //Task<List<UpdateUserSurveyAssessmentResponseVM>> UpdateUserSurveyAssessment(UpdateUserSurveyAssessmentCommand command);

        Task<List<AssessmentSurveysRespVm>> GetAssessmentSurveyByUsersurveyId(long id);
    }
}
