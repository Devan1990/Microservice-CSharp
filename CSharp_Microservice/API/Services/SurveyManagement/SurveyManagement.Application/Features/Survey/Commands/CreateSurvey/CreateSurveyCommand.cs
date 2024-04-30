using MediatR;
using System;

namespace SurveyManagement.Application.Features.Survey.Commands.CreateSurvey
{
    public class CreateSurveyCommand : IRequest<long>
    {

        public DateTime FromPeriod { get; set; }
        public DateTime ToPeriod { get; set; }
        public long RoleId { get; set; }
    }
}
