using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Commands.DeleteSurvey
{
    public class DeleteSurveyCommand : IRequest
    {
        public long Id { get; set; }
    }
}
