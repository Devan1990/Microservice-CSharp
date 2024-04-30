using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.Survey.Queries.GetSurvey
{
    public class GetSurveyByIdQuery : IRequest<SurveyVm>
    {
        public long Id { get; set; }

        public GetSurveyByIdQuery(long id)
        {
           Id = id;
        }       
    }
}
