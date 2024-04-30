using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Infrastructure;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Exceptions;
using SurveyManagement.Application.Features.Survey.Commands.UpdateSurvey;
using SurveyManagement.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand, long>
    {
        private readonly ISurveyRepository _surveyRepository;
       // private readonly ISurveyRoleMappingRepository _surveyRoleMappingRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<UpdateSurveyCommandHandler> _logger;

        public UpdateSurveyCommandHandler(ISurveyRepository surveyRepository, IMapper mapper, IEmailService emailService, ILogger<UpdateSurveyCommandHandler> logger)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
         //   _surveyRoleMappingRepository = surveyRoleMappingRepository ?? throw new ArgumentNullException(nameof(surveyRoleMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<long> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
        {
            var surveyToUpdate = await _surveyRepository.GetSurveyById(request.Id);
            var surveyquery = await _surveyRepository.GetSurveyQuery(a => a.RoleId == request.Id && a.FromPeriod.Year == request.FromPeriod.Year && a.ToPeriod.Year == request.ToPeriod.Year);
            
            if ((request.ToPeriod.Year - request.FromPeriod.Year) > 1)
            {
                throw new Exception("FY difference should not be greater than 1 year");
            }

            if (surveyquery.Count > 0)
            {
                throw new Exception("SurveyID already exists for given Role");
            }
            if (surveyToUpdate == null)
            {
                throw new NotFoundException(nameof(Survey), request.Id);
            }
           // var existingSurvey = surveyToUpdate..ToList();

            //var updatedSurvey = surveyToUpdate.SurveyRoleMappings.Select(a =>
            //{
            //    a.RoleId = request.Id;
            //    return a;
            //}).ToList();

            //foreach (var surveyentry in request.UpdateSurveyRoleMapVm)
            //{
            //    var map = updatedSurvey.FirstOrDefault(a => a.Id == surveyentry.id);

            //    if (map == null)
            //    {
            //        updatedSurvey.Add(_mapper.Map<SurveyRoleMapping>(surveyentry));
            //    }
            //    else
            //    {
            //        map.RoleId = surveyentry.id;
                
            //    }
            //}

            ////surveyToUpdate.SurveyRoleMappings = updatedSurvey;
            //await _surveyRepository.UpdateAsync(surveyToUpdate);



            _mapper.Map(request, surveyToUpdate, typeof(UpdateSurveyCommand), typeof(Survey));

           var entity= await _surveyRepository.UpdateSurvey(surveyToUpdate);
            _logger.LogInformation($"Survey {surveyToUpdate.Id} is successfully updated.");


             return entity;



        }
    }
}


//var existingMap = roleToUpdate.CompetenciesMap.ToList();

//var updatedMap = roleToUpdate.CompetenciesMap.Select(a =>
//{
//    a.IsSelected = false;
//    return a;
//}).ToList();

//foreach (var mapEntry in request.CompetenciesMap)
//{
//    var map = updatedMap.FirstOrDefault(a => a.Id == mapEntry.Id);

//    if (map == null)
//    {
//        updatedMap.Add(_mapper.Map<CompetenciesMap>(mapEntry));
//    }
//    else
//    {
//        map.IsSelected = mapEntry.IsSelected;
//        map.CompetencyLevelId = mapEntry.CompetencyLevelId;
//    }
//}

//roleToUpdate.CompetenciesMap = updatedMap;
//await _roleRepository.UpdateAsync(roleToUpdate);

//_logger.LogInformation($"Role {roleToUpdate.Id} is successfully updated.");

//return Unit.Value;
//        }