using CAT.BFF.Models;
using System.Text;

namespace CAT.BFF.Services
{
    public class SurveyManagementService : ISurveyManagementService
    {
        private readonly HttpClient _client;

        public SurveyManagementService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<List<UserSurveyVm>> GetAssessmentSurvey(GetUserSurveyAssessmentByIdsQuery command)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(command);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //var response = await _client.PostAsync($"/api/v1/AssessmentSurvey/GetAssessmentSurvey", data);
            var response = await _client.PostAsync($"/api/v1/UserSurvey/GetUserSurveyById", data);
            Console.Write(response.Content);
            var result = await response.ReadContentAs<List<UserSurveyVm>>();
            return result;
        }

        public async Task<List<UserSurveyVm>> GetUserSurvey()
        {
            var response = await _client.GetAsync($"/api/v1/UserSurvey/GetUserSurvey");
            var result = await response.ReadContentAs<List<UserSurveyVm>>();
            return result;
        }

        public async Task<List<CaptureAssessmentSurveyResponseVM>> CreateAssessmentSurvey(CreateAssessmentSurveyCommand command)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(command);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //var response = await _client.PostAsync($"/api/v1/AssessmentSurvey/GetAssessmentSurvey", data);
            var response = await _client.PostAsync($"/api/v1/AssessmentSurvey/CreateAssessmentSurvey", data);
            Console.Write(response.Content);
            var result = await response.ReadContentAs<List<CaptureAssessmentSurveyResponseVM>>();
            return result;
        }


        //public async Task<List<UpdateUserSurveyAssessmentResponseVM>> UpdateUserSurveyAssessment(UpdateUserSurveyAssessmentCommand command)
        //{
        //    var json = Newtonsoft.Json.JsonConvert.SerializeObject(command);
        //    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
        //    //var response = await _client.PostAsync($"/api/v1/AssessmentSurvey/GetAssessmentSurvey", data);
        //    var response = await _client.PutAsync($"/api/v1/UserSurvey/UpdateUserSurvey", data); 
        //    Console.Write(response.Content);
        //    var result = await response.ReadContentAs<List<UpdateUserSurveyAssessmentResponseVM>>();
        //    return result;
        //}

        public async Task<List<AssessmentSurveysRespVm>> GetAssessmentSurveyByUsersurveyId(long id)
        {
            var response = await _client.GetAsync($"/api/v1/AssessmentSurvey/GetAssessmentSurveyByUsersurveyId/"+id);
            var result = await response.ReadContentAs<List<AssessmentSurveysRespVm>>();
            return result;
        }
    }
}
