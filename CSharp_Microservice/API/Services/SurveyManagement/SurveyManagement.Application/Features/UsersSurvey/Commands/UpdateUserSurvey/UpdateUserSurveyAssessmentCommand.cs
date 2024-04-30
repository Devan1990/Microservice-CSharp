using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Commands.UpdateUserSurvey
{
    public class UpdateUserSurveyAssessmentCommand : IRequest<List<UpdateUserSurveyAssessmentResponseVM>>
    {
        public ICollection<UpdateUserSurveyAssessment> UpdateAssessment { get; set; }
    }
}
