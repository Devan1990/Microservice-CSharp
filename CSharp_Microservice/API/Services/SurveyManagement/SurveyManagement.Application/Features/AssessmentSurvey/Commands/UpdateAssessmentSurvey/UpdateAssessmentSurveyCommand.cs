using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Commands.UpdateAssessmentSurvey
{
    public class UpdateAssessmentSurveyCommand : IRequest
    {
        public ICollection<UpdateAssessmentSurvey> assessmentsurvey { get; set; }
    }
}
