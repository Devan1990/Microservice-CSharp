using SurveyManagement.Grpc;
using System;
using System.Threading.Tasks;

namespace UserManagement.API.GrpcServices
{
    public class SurveyListGrpcService
    {
        private readonly SurveyManagementProtoService.SurveyManagementProtoServiceClient _roleProtoService;
        public SurveyListGrpcService(SurveyManagementProtoService.SurveyManagementProtoServiceClient roleProtoService)
        {
            _roleProtoService = roleProtoService ?? throw new ArgumentNullException(nameof(roleProtoService));

        }

        public async Task<GetSurveyListRes> GetSurveylistBy(string CurrFY)
        {
            var roleRequest = new GetSurveyListRequest { SurveyName = CurrFY };

            return await _roleProtoService.GetSurveylistByAsync(roleRequest);
        }
    }
}
