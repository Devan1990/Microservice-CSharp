using CompetencyFramework.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompetencyFramework.API.GrpcServices
{
    public class CompetencyFrameworkGrpcService
    {

        private readonly CompetencyFrameworkProtoService.CompetencyFrameworkProtoServiceClient _competencyFrameworkeProtoService;

        public CompetencyFrameworkGrpcService(CompetencyFrameworkProtoService.CompetencyFrameworkProtoServiceClient competencyFrameworkeProtoService)
        {
            _competencyFrameworkeProtoService = competencyFrameworkeProtoService ?? throw new ArgumentNullException(nameof(competencyFrameworkeProtoService));
        }

        public async Task<IsCompetencyExistsValue> IsCompetencyExists(long id)
        {
            var competencyeRequest = new GetCompetencyRequest { CompetencyId = id };

            return await _competencyFrameworkeProtoService.IsCompetencyExistsAsync(competencyeRequest);
        }

        public async Task<IsCompetencyGroupExistsValue> IsCompetencyGroupExists(long id)
        {
            var competencyGroupRequest = new GetCompetencyGroupRequest { CompetencyGroupId = id };

            return await _competencyFrameworkeProtoService.IsCompetencyGroupExistsAsync(competencyGroupRequest);
        }

        public async Task<GetCompetencyDetailsResponse> GetCompetencyDetails(List<long> ids)
        {
            var competencyRequest = new GetCompetenciesRequest();
            competencyRequest.Ids.Add(ids);
            return await _competencyFrameworkeProtoService.GetCompetencyDetailsAsync(competencyRequest);
        }
    }
}
