using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.AssessmentSurvey.Commands.CreateAssessmentSurvey
{
    public class CreateAssessmentSurveyCommand : IRequest<List<AssessmentSurveyResponseVM>>
    {
       public  ICollection<CaptureAssessmentSurveyVm2> assessmentsurvey { get; set; }
    }
}
