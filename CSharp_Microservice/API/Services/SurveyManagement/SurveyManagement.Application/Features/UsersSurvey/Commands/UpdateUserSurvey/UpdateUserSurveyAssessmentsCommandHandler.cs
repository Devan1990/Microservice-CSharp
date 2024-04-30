using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Exceptions;
using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Commands.UpdateUserSurvey
{
    public class UpdateUserSurveyAssessmentsCommandHandler : IRequestHandler<UpdateUserSurveyAssessmentCommand,  List<UpdateUserSurveyAssessmentResponseVM>>
    {
        private readonly IUserSurveyAssessmentRepository _userSurveyassessmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserSurveyAssessmentsCommandHandler> _logger;
        private readonly IUserSurveyRepository _userSurveyRepository;
        public UpdateUserSurveyAssessmentsCommandHandler(IUserSurveyAssessmentRepository userSurveyassessmentRepository, IMapper mapper, ILogger<UpdateUserSurveyAssessmentsCommandHandler> logger, IUserSurveyRepository userSurveyRepository)
        {
            _userSurveyassessmentRepository = userSurveyassessmentRepository ?? throw new ArgumentNullException(nameof(userSurveyassessmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userSurveyRepository = userSurveyRepository ?? throw new ArgumentNullException(nameof(userSurveyRepository));
        }
      public async Task<List<UpdateUserSurveyAssessmentResponseVM>> Handle(UpdateUserSurveyAssessmentCommand request, CancellationToken cancellationToken)
        {
            List<UpdateUserSurveyAssessmentResponseVM> responseVMs = new List<UpdateUserSurveyAssessmentResponseVM>();
            foreach (var map in request.UpdateAssessment)
            {
                try
                {
                    var assessmentsToUpdate = await _userSurveyassessmentRepository.GetByIdAsync(map.Id);

                    if (assessmentsToUpdate == null)
                    {
                        throw new NotFoundException(nameof(UserSurveyAssessment), map.Id);

                    }
                    //if (assessmentsToUpdate.AssessorId != 0)
                    //{
                    //    map.AssessorId = assessmentsToUpdate.AssessorId;
                    //}
                    _mapper.Map(map, assessmentsToUpdate, typeof(UpdateUserSurveyAssessment), typeof(UserSurveyAssessment));
                    var assessmentype = await _userSurveyRepository.GetAssessmentTypeById(map.AssessmentTypeId);
                    if (assessmentype == null)
                    {
                        throw new NotFoundException(nameof(AssessmentType), map.AssessmentTypeId);

                    }
                    AssessmentType asstype = new AssessmentType();
                    asstype = assessmentype;
                    assessmentsToUpdate.AssessmentType = asstype;
                    await _userSurveyassessmentRepository.UpdateAsync(assessmentsToUpdate);
                    UpdateUserSurveyAssessmentResponseVM res = new UpdateUserSurveyAssessmentResponseVM();
                    res.Id = map.Id;
                    res.Remarks = "Update Successfully";
                    responseVMs.Add(res);
                    _logger.LogInformation($"Assessments is successfully updated.");
                }
                catch(Exception ex)
                {
                    UpdateUserSurveyAssessmentResponseVM res = new UpdateUserSurveyAssessmentResponseVM();
                    res.Id = map.Id;
                    res.Remarks = "Error Occured";
                    responseVMs.Add(res);
                }
            }
            return responseVMs;
        }
    }
}
