using AutoMapper;
using Grpc.Core;
using UserManagement.Application.Contracts.Persistence;

using UserManagement.Grpc.Protos;

namespace UserManagement.Grpc.Services
{
    public class RoleService : RoleProtoService.RoleProtoServiceBase
    {
        private readonly IRoleRepository _repository;
        private readonly IUserRepository _userrepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;
 
        private readonly IRoleMappingRepository _rolemappingrepository;
        public RoleService(IRoleRepository repository, IUserRepository userRepository, IRoleMappingRepository rolemappingrepository, IMapper mapper, ILogger<RoleService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userrepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));    
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
          
            _rolemappingrepository = rolemappingrepository ?? throw new ArgumentNullException(nameof(rolemappingrepository));
        }

        public override async Task<IsRoleExistsValue> IsRoleExists(GetRoleRequest request, ServerCallContext context)
        {
            var role = await _repository.GetRoleById(request.RoleId);

            if (role == null)
            {
                return new IsRoleExistsValue() { Exists = false };
            }
            else
            {
                return new IsRoleExistsValue() { Exists = true };
            }
        }

        public override async Task<RoleModel> GetRoles(GetRoleRequest request, ServerCallContext context)
        {
            try
            {
                var role = await _repository.GetRoles();

                if (role == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"RoleName with RoleName={request.RoleName} is not found."));

                }
                //   _logger.LogInformation("RoleName is retrieved for RoleName : {roleName}, RoleId : {roleid}", role., role.RoleId);
                var surveyList = role.ToList();
                var response = new RoleModel();
                for (int i = 0; i < surveyList.Count; i++)
                {
                    var RoleDetails = new RoleDetails()
                    {
                        Id = surveyList[i].Id,
                       RoleId = surveyList[i].RoleId,
                       RoleName = surveyList[i].RoleName

                    };
                    response.RoleDetails.Add(RoleDetails);
                  
                }

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public override async Task<GetRolebyIDModel> GetRolesList(GetRolebyIDRequest request, ServerCallContext context)
        {
            try
            {
                var role = await _repository.GetRoleById(request.RoleId);

                if (role == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"RoleName with RoleName={request.RoleName} is not found."));

                }
                _logger.LogInformation("RoleName is retrieved for RoleName : {roleName}, RoleId : {roleid}", role.RoleName, role.RoleId);
                GetRolebyIDModel GRM = new GetRolebyIDModel();
                GRM.Id = role.Id;
                GRM.RoleId = role.RoleId;
                GRM.RoleName = role.RoleName;
                var roleModel = GRM;
                return roleModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public override async Task<GetUserbyIDModel> GetUsersAssesseeList(GetUserbyIDRequest request, ServerCallContext context)
        //{
        //    try
        //    {
        //        var user = await _userrepository.GetUsersAssesseeList();

        //        if (user == null)
        //        {
        //            throw new RpcException(new Status(StatusCode.NotFound, $"UserName with UserName={request.FirstName} is not found."));

        //        }
        //        var ExpectedValue = await _repository.GetExpectedLevels();
        //        var ExpectedValueList = ExpectedValue.ToList();
        //        var userlist = user.ToList();
        //        var response = new GetUserbyIDModel();

        //        for (int i = 0; i < userlist.Count; i++)
        //        {
        //            for (int j = 0; j < ExpectedValueList.Count; j++)
        //            {

        //                if (userlist[i].Role.CompetenciesMap.ToList()[0].ExpectedLevelId == ExpectedValueList[j].Id)
        //                {

        //                    var userValues = new GetSurveysUserListResponse()
        //                    {
        //                        Id = userlist[i].Id,
        //                        EmployeeName = userlist[i].FirstName + " " + userlist[i].LastName,
        //                        BenchMarkId = userlist[i].Role.CompetenciesMap.ToList()[0].ExpectedLevelId,
        //                        BenchMark = ExpectedValueList[j].ExpectedLevelName,
        //                        UserId = userlist[i].UserId,
        //                        RoleName = userlist[i].Role.RoleName,
        //                        RoleId = userlist[i].Role.RoleId,
        //                    };
        //                    response.SurveyUserlst.Add(userValues);
        //                }
        //            }
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        //public override async Task<GetUserbyIDModel> GetUserSurveyLst(GetUserSurveyReq request, ServerCallContext context)
        //{
        //    try
        //    {
        //        List<long> lst = new List<long>();
        //        var reqlist = request.AssessmentID.ToList();
        //        foreach (var item in reqlist)
        //        {
        //            long id = 0;
        //            id = item.Id;
        //            lst.Add(id);
        //        }
        //        var usersurvey = await _usersurveyrepository.GetUserSurveyList(lst);
        //        var usersurvyrres = from us in usersurvey
        //                            join SL in reqlist on us.Id equals SL.Id
        //                            select new
        //                            {
        //                                usersurveyid = us.Id,
        //                                Id = us.User.Id,
        //                                Userid = us.User.UserId
        //                            };
        //        if (usersurvyrres.Count() == 0)
        //        {
        //            throw new RpcException(new Status(StatusCode.NotFound, $"Given Assessment Survey is not found."));
        //        }
        //        var user1 = await _userrepository.GetUsersAssesseeList();

        //        var user = from us in usersurvyrres
        //                   join SL in user1 on us.Id equals SL.Id
        //                   select new
        //                   {
        //                       usersurveyid = us.usersurveyid,
        //                       Id = us.Id,
        //                       UserId = us.Userid,
        //                       Role = SL.Role,
        //                       FirstName = SL.FirstName,
        //                       LastName = SL.LastName
        //                   };
        //        var ExpectedValue = await _repository.GetExpectedLevels();
        //        var ExpectedValueList = ExpectedValue.ToList();
        //        var userlist = user.ToList();
        //        var response = new GetUserbyIDModel();

        //        for (int i = 0; i < userlist.Count; i++)
        //        {
        //            for (int j = 0; j < ExpectedValueList.Count; j++)
        //            {
        //                if (userlist[i].Role.CompetenciesMap.ToList()[0].ExpectedLevelId == ExpectedValueList[j].Id)
        //                {
        //                    var CompentcyMap = new CompetenciesMapList();
        //                    foreach (var rol in userlist[i].Role.CompetenciesMap)
        //                    {
        //                        var compmaplist = new CompetenciesMap()
        //                        {
        //                            CompetencyGroupId = rol.CompetencyGroupId,
        //                            ExpectedLevelId = rol.ExpectedLevelId,
        //                            CompetencyId = rol.CompetencyId,
        //                            IsSelected = rol.IsSelected
        //                        };
        //                        CompentcyMap.CompetenciesMap.Add(compmaplist);
        //                    }
        //                    var userValues = new GetSurveysUserListResponse()
        //                    {
        //                        Usersurveyid = userlist[i].usersurveyid,
        //                        Id = userlist[i].Id,
        //                        EmployeeName = userlist[i].FirstName + " " + userlist[i].LastName,
        //                        BenchMarkId = userlist[i].Role.CompetenciesMap.ToList()[0].ExpectedLevelId,
        //                        BenchMark = ExpectedValueList[j].ExpectedLevelName,
        //                        UserId = userlist[i].UserId,
        //                        RoleName = userlist[i].Role.RoleName,
        //                        RoleId = userlist[i].Role.RoleId,
        //                        RoleTId = userlist[i].Role.Id,
        //                        CompetenciesMapList = CompentcyMap
        //                    };
        //                    response.SurveyUserlst.Add(userValues);
        //                }
        //            }
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        public override async Task<GetUserByRoleIdModel> GetUserByRoleIdLst(GetUserByRoleIdReq request, ServerCallContext context)
        {
            try
            {
                var users = await _userrepository.GetUserByRoleId(request.RoleId);

                if (users == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"RoleID ={request.RoleId} is not found."));

                }
                //   _logger.LogInformation("RoleName is retrieved for RoleName : {roleName}, RoleId : {roleid}", role., role.RoleId);
                var usersList = users.ToList();
                var response = new GetUserByRoleIdModel();
                for (int i = 0; i < usersList.Count; i++)
                {
                    var UserDetails = new GetUserIDData()
                    {
                        Id = usersList[i].Id,
                        RoleId = usersList[i].Role.Id,
                    };
                    response.Userrolesdetails.Add(UserDetails);

                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public override async Task<GetRoleMappingList> GetRoleMappingLst(GetRoleMappingReq request,ServerCallContext context)
        {
            try
            {
                var RoleMapping = await _rolemappingrepository.GetRoleMappings();

                if (RoleMapping == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"RoleID ={request.Id} is not found."));

                }
                //   _logger.LogInformation("RoleName is retrieved for RoleName : {roleName}, RoleId : {roleid}", role., role.RoleId);
                var RoleMappingList = RoleMapping.ToList();
                var response = new GetRoleMappingList();
                for (int i = 0; i < RoleMappingList.Count; i++)
                {
                    var rolmapdata = new GetRoleMappingData();
                    rolmapdata.Id = RoleMappingList[i].Id;
                    rolmapdata.RoleId = RoleMappingList[i].RoleId;
                    var AssessorRoleList = RoleMappingList[i].AssessorRole.ToList();
                    
                    var GetassessorRole = new GetassessorRole();
                    for (int j = 0; j < AssessorRoleList.Count; j++)
                    {
                        var GetAssessorData = new GetAssessorData();
                        GetAssessorData.Id = AssessorRoleList[j].Id;
                        GetAssessorData.Mandatory = AssessorRoleList[j].Mandatory;
                        GetAssessorData.RoleId = AssessorRoleList[j].RoleId;
                        GetassessorRole.AssessorRole.Add(GetAssessorData);
                    }
                    rolmapdata.AssessorRole = GetassessorRole;
                    response.RoleMappingData.Add(rolmapdata);

                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public override async Task<GetUserDataResponse> GetUserLst(GetUserDataReq request, ServerCallContext context)
        {
            try
            {
                var users = await _userrepository.GetUsers();

                if (users == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"RoleID ={request.Id} is not found."));

                }
                //   _logger.LogInformation("RoleName is retrieved for RoleName : {roleName}, RoleId : {roleid}", role., role.RoleId);
                var usersList = users.ToList();
                var response = new GetUserDataResponse();
                for (int i = 0; i < usersList.Count; i++)
                {
                    var userdata = new GetUserData();
                    userdata.Id = usersList[i].Id;
                    userdata.UserId = usersList[i].UserId;
                    userdata.FirstName = usersList[i].FirstName;
                    userdata.LastName = usersList[i].LastName;
                    userdata.RoleId = usersList[i].Role.Id;
                    userdata.RoleName = usersList[i].Role.RoleName;
                    userdata.CountryName = usersList[i].Country.CountryName;
                    userdata.AreaName = usersList[i].Area.AreaName;
                    userdata.RegionName = usersList[i].Region.RegionName;
                   
                    response.Users.Add(userdata);

                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
