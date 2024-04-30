using AutoMapper;
using MediatR;
using SurveyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class GetAssessmentTypeListQueryHandler : IRequestHandler<GetAssessmentTypeListQuery, List<AssessmentTypeVM>>
    {
        private readonly IUserSurveyRepository _usersurveyrepository;
        private readonly IMapper _mapper;
        public GetAssessmentTypeListQueryHandler(IUserSurveyRepository usersurveyrepository, IMapper mapper)
        {
            _usersurveyrepository = usersurveyrepository ?? throw new ArgumentNullException(nameof(usersurveyrepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<List<AssessmentTypeVM>> Handle(GetAssessmentTypeListQuery request, CancellationToken cancellationToken)
        {
            var assessmenttype = await _usersurveyrepository.GetAssessmentType();
            return _mapper.Map<List<AssessmentTypeVM>>(assessmenttype);
        }
    }
}
