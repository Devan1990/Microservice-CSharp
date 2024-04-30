using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Infrastructure;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SurveyManagement.Domain.Entities;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Commands.CreateAssessmentSurvey
{
    public class CreateAssessmentSurveyCommandHandler : IRequestHandler<CreateAssessmentSurveyCommand, List<AssessmentSurveyResponseVM>>
    {
            private readonly IAssessmentSurveyRepository _assessmentSurveyRepository;
            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;
            private readonly ILogger<CreateAssessmentSurveyCommandHandler> _logger;

            public CreateAssessmentSurveyCommandHandler(IAssessmentSurveyRepository assessmentSurveyRepository,  IMapper mapper, IEmailService emailService, ILogger<CreateAssessmentSurveyCommandHandler> logger)
            {
               
                _assessmentSurveyRepository = assessmentSurveyRepository ?? throw new ArgumentNullException(nameof(assessmentSurveyRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<List<AssessmentSurveyResponseVM>> Handle(CreateAssessmentSurveyCommand request, CancellationToken cancellationToken)
        {
            // throw new BadRequestException();
            Domain.Entities.AssessmentSurvey entity = new Domain.Entities.AssessmentSurvey();
            List<AssessmentSurveyResponseVM> ASR = new List<AssessmentSurveyResponseVM>();
            foreach (var map in request.assessmentsurvey)
            {
                AssessmentSurveyResponseVM Asssurres= new AssessmentSurveyResponseVM();
                var assessmentsurvey = _mapper.Map<Domain.Entities.AssessmentSurvey>(map);
                _mapper.Map(request, assessmentsurvey, typeof(CreateAssessmentSurveyCommand), typeof(Domain.Entities.AssessmentSurvey));
                assessmentsurvey.UserSurveyAssessmentId = map.UserSurveyAssessmentId;
                assessmentsurvey.UserId = map.UserId;
                // assessmentsurvey.EmployeeName = map.EmployeeName;
                assessmentsurvey.RoleId = map.RoleId;
                assessmentsurvey.CompetencyGroupId = map.CompetencyGroupId;
                assessmentsurvey.CompetencyId = map.CompetencyId;
                assessmentsurvey.BenchMarkId = map.BenchMarkId;
                assessmentsurvey.ActualLevelId = map.ActualLevelId;
                var responseID = await _assessmentSurveyRepository.AddAssessmentSurvey(assessmentsurvey, map.AssessmentTypeId);
                Asssurres.UserSurveyAssessmentId = map.UserSurveyAssessmentId;
                Asssurres.AssessmentTypeId = map.AssessmentTypeId;
                Asssurres.Remarks = "Successfully Added.";
                ASR.Add(Asssurres);
                _logger.LogInformation($"AssessmenSurveys is successfully updated.");
            }
            return ASR;

        }
    }
    
}
