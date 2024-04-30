using AutoMapper;
using Grpc.Core;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Domain.Entities;
using SurveyManagement.Grpc;
namespace SurveyManagement.Grpc.Services
{
    public class SurveyManagementService : SurveyManagementProtoService.SurveyManagementProtoServiceBase
    {
        private readonly ISurveyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SurveyManagementService> _logger;
        public SurveyManagementService(ISurveyRepository repository, IMapper mapper, ILogger<SurveyManagementService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<GetSurveyRes> GetSurveylistByCurrentFinancialYear(GetSurveyRequest request, ServerCallContext context)
        {
            var role = await _repository.GetSurveyByCurrentFinancialYear(request.CFY);
            var surveyList = role.ToList();
            var response = new GetSurveyRes();
            for (int i = 0; i < surveyList.Count; i++)
            {
                //var surveyRoleMapList = surveyList[i].SurveyRoleMappings.ToList();
                //for (int j = 0; j < surveyRoleMapList.Count; j++)
                //{
                    var SuveryRoleValue = new SuveryRoleValue()
                    {
                        SurveyId = surveyList[i].Id,
                        RoleId = surveyList[i].RoleId,
                    };
                    response.Surveylst.Add(SuveryRoleValue);
                  }
            return response;
        }
        public override async Task<GetSurveyListRes> GetSurveylistBy(GetSurveyListRequest request, ServerCallContext context)
        {
            var role = await _repository.GetSurvey();
            var surveyList = role.ToList();
            var response = new GetSurveyListRes();
            for (int i = 0; i < surveyList.Count; i++)
            {
            //    var surveyRoleMapList = surveyList[i].SurveyRoleMappings.ToList();
            //for (int j = 0; j < surveyRoleMapList.Count; j++)
            //{
                var SuveryRoleValue = new GetSurveyListResponse()
                {
                    SurveyId = surveyList[i].SurveyId,
                    Id = surveyList[i].Id,
                };
                response.Surveylst.Add(SuveryRoleValue);
                }

            
            return response;
        }
    }
}