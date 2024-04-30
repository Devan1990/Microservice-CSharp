using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Exceptions;
using SurveyManagement.Application.Features.AssessmentSurvey.Commands.UpdateAssessmentSurvey;
using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.UsersSurvey.Commands.UpdateAssessmentSurvey
{
    public class UpdateAssessmentSurveyCommandHandler : IRequestHandler<UpdateAssessmentSurveyCommand>
    {
        private readonly IAssessmentSurveyRepository _assessmentsurveyrepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAssessmentSurveyCommandHandler> _logger;
        public UpdateAssessmentSurveyCommandHandler(IAssessmentSurveyRepository assessmentsurveyrepository, IMapper mapper, ILogger<UpdateAssessmentSurveyCommandHandler> logger)
        {
            _assessmentsurveyrepository = assessmentsurveyrepository ?? throw new ArgumentNullException(nameof(assessmentsurveyrepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Unit> Handle(UpdateAssessmentSurveyCommand request, CancellationToken cancellationToken)
        {
            foreach (var map in request.assessmentsurvey)
            {
                var assessmentsToUpdate = await _assessmentsurveyrepository.GetAssessmentSurveyById(map.Id);
                
                if (assessmentsToUpdate == null)
                {
                    throw new NotFoundException(nameof(AssessmentSurvey), map.UserSurveyId);

                }
                _mapper.Map(request, assessmentsToUpdate, typeof(UpdateAssessmentSurveyCommand), typeof(AssessmentSurvey));
                assessmentsToUpdate.UserSurveyAssessmentId = map.UserSurveyId;
                assessmentsToUpdate.ActualLevelId = map.ActualLevelId;
                assessmentsToUpdate.BenchMarkId = map.BenchMarkId;
                assessmentsToUpdate.RoleId = map.RoleId;
                await _assessmentsurveyrepository.UpdateAsync(assessmentsToUpdate);

                _logger.LogInformation($"Assessments is successfully updated.");
            }
            return Unit.Value;  
        }
    }
}
