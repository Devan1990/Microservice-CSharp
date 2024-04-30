
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetAssessmentSurvey;
using UserManagement.Grpc.Protos;

namespace SurveyManagement.API.GrpcServices
{
    public class RoleGrpcService
    {

        private readonly RoleProtoService.RoleProtoServiceClient _roleProtoService;

        public RoleGrpcService(RoleProtoService.RoleProtoServiceClient roleProtoService)
        {
            _roleProtoService = roleProtoService ?? throw new ArgumentNullException(nameof(roleProtoService));
        }

        public async Task<RoleModel> GetRoles(string RoleName)
        {
            var roleRequest = new GetRoleRequest { RoleName = RoleName };

            return await _roleProtoService.GetRolesAsync(roleRequest);
        }

        public async Task<IsRoleExistsValue> IsRoleExistsValue(long id)
        {
            var roleRequest = new GetRoleRequest { RoleId = id };

            return await _roleProtoService.IsRoleExistsAsync(roleRequest);

        }
        public async Task<GetRolebyIDModel> GetRolesList(long id)
        {
            var roleRequest = new GetRolebyIDRequest { RoleId = id };

            return await _roleProtoService.GetRolesListAsync(roleRequest);
        }
        //public async Task<GetUserbyIDModel> GetUsersList()
        //{
        //    var userRequest = new GetUserbyIDRequest();

        //    return await _roleProtoService.GetUsersAssesseeListAsync(userRequest);
        //}

        //public async Task<GetUserbyIDModel> GetUserssurveyList(List<AssessmentSurveyVm> lst)
        //{
        //    var userRequest = new GetUserSurveyReq();

        //    foreach (var item in lst)
        //    {
        //        var userRequest1 = new GetUserSurveysLstReq();
        //        userRequest1.Id = item.Id;
        //        userRequest.AssessmentID.Add(userRequest1);
        //    }
        //    return await _roleProtoService.GetUserSurveyLstAsync(userRequest);
        //}

        public async Task<GetUserDataResponse> GetAllUsersList()
        {
            var userRequest = new GetUserDataReq();

            return await _roleProtoService.GetUserLstAsync(userRequest);
        }
        public async Task<GetRoleMappingList> GetRoleMappingLst(long Id)
        {
            var roleRequest = new GetRoleMappingReq { Id = Id };

            return await _roleProtoService.GetRoleMappingLstAsync(roleRequest);
        }
    }
}
