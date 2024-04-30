using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Infrastructure;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Commands.DeleteSurvey
{
    public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand>
    {
        private readonly ISurveyRepository _surveyRepository;
      //  private readonly ISurveyRoleMappingRepository _surveyRoleMappingRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<DeleteSurveyCommandHandler> _logger;

        public DeleteSurveyCommandHandler(ISurveyRepository surveyRepository, IMapper mapper, IEmailService emailService, ILogger<DeleteSurveyCommandHandler> logger)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
          //  _surveyRoleMappingRepository = surveyRoleMappingRepository ?? throw new ArgumentNullException(nameof(surveyRoleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
        {
            var SurveyToDelete = await _surveyRepository.GetByIdAsync(request.Id);
            if (SurveyToDelete == null)
            {
                throw new NotFoundException(nameof(Survey), request.Id);
            }
            SurveyToDelete.IsDeleted=true;

            await _surveyRepository.UpdateAsync(SurveyToDelete);
            _logger.LogInformation($"Survey  {SurveyToDelete.Id} is successfully deleted.");
            return Unit.Value;
        }
    }
}
