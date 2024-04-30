using MediatR;
using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SurveyManagement.Application.Features.Survey.Commands.UpdateSurvey
{
    public class UpdateSurveyCommand : IRequest<long>
    {
        public long Id { get; set; }
        public DateTime FromPeriod { get; set; }
        public DateTime ToPeriod { get; set; }
        public long RoleId { get; set; }
      //  public ICollection<UpdateSurveyRoleMapVm> UpdateSurveyRoleMapVm { get; set; }


    }
   
}
