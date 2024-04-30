using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Infrastructure;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Exceptions;
using SurveyManagement.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Commands.CreateSurvey
{
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, long>
    {
        private readonly ISurveyRepository _surveyRepository;
      //  private readonly ISurveyRoleMappingRepository _surveyRoleMappingRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateSurveyCommandHandler> _logger;

        public CreateSurveyCommandHandler(ISurveyRepository surveyRepository,  IMapper mapper, IEmailService emailService, ILogger<CreateSurveyCommandHandler> logger)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
           // _surveyRoleMappingRepository = surveyRoleMappingRepository ?? throw new ArgumentNullException(nameof(surveyRoleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<long> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = (await _surveyRepository.GetAsync(a => ((a.FromPeriod.Year == request.FromPeriod.Year) && (a.ToPeriod.Year == request.ToPeriod.Year) && a.RoleId == request.RoleId))).FirstOrDefault();

            var surveyquery = await _surveyRepository.GetSurveyQuery(a => a.RoleId == request.RoleId && a.FromPeriod.Year == request.FromPeriod.Year && a.ToPeriod.Year == request.ToPeriod.Year);
            //var Fromdate = await _surveyRepository.GetAsync(a => a.FromPeriod.Year == request.FromPeriod.Year);
            //var Todate = await _surveyRepository.GetAsync(a => a.ToPeriod.Year == request.ToPeriod.Year);

            if ((request.ToPeriod.Year - request.FromPeriod.Year) > 1)
            {
                throw new Exception("FY difference should not be greater than 1 year");
            }

            if (surveyquery.Count > 0)
            {
                throw new Exception("SurveyID already exists for given Role");
            }


            if (survey == null)
            {
                survey = _mapper.Map<Domain.Entities.Survey>(request);
            }
           

            if (survey == null)
            {
                throw new BadRequestException();
            }

            //var surveyRoleMapping = new SurveyRoleMapping() { RoleId = request.RoleId };
            //surveyRoleMapping.Survey = survey;
            var entity = await _surveyRepository.AddSurvey(survey);

            _logger.LogInformation($"Survey  {entity} is successfully created.");

            return entity;
        }

        //private async Task SendMail(Domain.Entities.Survey order)
        //{
        //    var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

        //    try
        //    {
        //        await _emailService.SendEmail(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
        //    }
        //}
    }
}

