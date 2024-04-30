using AutoMapper;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
using CompetencyFramework.Grpc.Protos;
using Google.Protobuf.Collections;
using Grpc.Core;

namespace CompetencyFramework.Grpc.Services
{
    public class CompetencyFrameworkService : CompetencyFrameworkProtoService.CompetencyFrameworkProtoServiceBase
    {
        private readonly ICompetencyRepository _repository;
        private readonly ICompetencyGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompetencyFrameworkService> _logger;

        public CompetencyFrameworkService(ICompetencyRepository repository, ICompetencyGroupRepository groupRepository, IMapper mapper, ILogger<CompetencyFrameworkService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository)); 
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<IsCompetencyExistsValue> IsCompetencyExists(GetCompetencyRequest request, ServerCallContext context)
        {
            var competency = await _repository.GetCompetencyById(request.CompetencyId);

            if (competency == null)
            {
                return new IsCompetencyExistsValue() { Exists = false };
            }
            else
            {
                return new IsCompetencyExistsValue() { Exists = true };
            }
        }
        public override async Task<IsCompetencyGroupExistsValue> IsCompetencyGroupExists(GetCompetencyGroupRequest request, ServerCallContext context)
        {
            var competency = await _groupRepository.GetCompetencyGroupById(request.CompetencyGroupId);

            if (competency == null)
            {
                return new IsCompetencyGroupExistsValue() { Exists = false };
            }
            else
            {
                return new IsCompetencyGroupExistsValue() { Exists = true };
            }
        }
        public override async Task<GetCompetencyDetailsResponse> GetCompetencyDetails(GetCompetenciesRequest request, ServerCallContext context)
        {
            var competencies = await _groupRepository.GetCompetencyGroupListById(request.Ids.ToList());

            if (competencies == null)
            {
                throw new NotFoundException("CompetencyGroup", request.Ids);
            }

            var response = new GetCompetencyDetailsResponse();

            competencies.ToList().ForEach(competency =>

            {
                var competencyGroupValue = new CompetencyGroupValue()
                {
                    Id = competency.Id,
                    CompetencyGroupId = competency.CompetencyGroupId,
                    Name = competency.Name,
                    Description = competency.Description,
                     

                };
                foreach (var competencie in competency.Competencies)
                {

                    competencyGroupValue.Competencies.Add(new CompetencyValue()
                    {
                        Id = competencie.Id,
                        CompetencyId = competencie.CompetencyId,
                        Name = competencie.Name,
                        Description = competencie.Description,
                        ExpectedLevelID = 0,
                        ExpectedLevelName = "",
                        IsSelected = false,



                    });

                };
                for (int i = 0; i < competencyGroupValue.Competencies.Count; i++)
                {

                    foreach (var atrributes in competency.Competencies.ToList()[i].Attributes)
                    {
                        var cl = competency.Competencies.ToList()[i].Attributes.ToList()[0].CompetencyLevel;
                        var competencyLevel = new CompetencyLevelValue()
                        {
                            Id = cl.Id,
                            Name = cl.Name,
                            Weightage = cl.Weightage

                        };
                        var competency_Level = new CompetencyLevelValue();
                        competency_Level.Id = cl.Id;
                        competency_Level.Name = cl.Name;
                        competency_Level.Weightage = cl.Weightage;
                        competencyGroupValue.Attributes.Add(new AttributeValue()
                        {

                            Id = atrributes.Id,
                            Description = atrributes.Description,
                            CompetencyLevel = competency_Level


                        });
                    };

                }

                response.CompetencyGroups.Add(competencyGroupValue);
            });

            return response;
        }
    }
}
