using SurveyManagement.WorkerService.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.WorkerService.GrpcServices
{
    public class UserSurveyGrpcService
    {
        private readonly RoleProtoService.RoleProtoServiceClient _roleProtoService;
        public UserSurveyGrpcService(RoleProtoService.RoleProtoServiceClient roleProtoService)
        {
            _roleProtoService = roleProtoService ?? throw new ArgumentNullException(nameof(roleProtoService));
        }
        public async Task<GetUserByRoleIdModel> GetUserByRoleIdLst(long Id)
        {
            var roleRequest = new GetUserByRoleIdReq { RoleId = Id };

            return await _roleProtoService.GetUserByRoleIdLstAsync(roleRequest);
        }

        public async Task<GetRoleMappingList> GetRoleMappingLst(long Id)
        {
            var roleRequest = new GetRoleMappingReq { Id = Id };

            return await _roleProtoService.GetRoleMappingLstAsync(roleRequest);
        }
    }
}
