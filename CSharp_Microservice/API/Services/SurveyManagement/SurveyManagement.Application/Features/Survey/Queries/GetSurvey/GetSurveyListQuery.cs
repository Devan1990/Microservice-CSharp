using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Queries.GetSurvey
{
    public class GetSurveyListQuery : IRequest<List<SurveyVm>>
    {
        public GetSurveyListQuery()
        {

        }
    }
}
