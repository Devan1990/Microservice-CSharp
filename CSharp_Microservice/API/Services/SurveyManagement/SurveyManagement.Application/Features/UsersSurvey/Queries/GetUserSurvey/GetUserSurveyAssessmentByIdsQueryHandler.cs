using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class GetUserSurveyAssessmentByIdsQueryHandler : IRequestHandler<GetUserSurveyAssessmentByIdsQuery, List<UserSurveyAssessmentSurveyVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUserSurveyAssessmentRepository _usersurveyassessmentrepository;
        public GetUserSurveyAssessmentByIdsQueryHandler(IMapper mapper, IUserSurveyAssessmentRepository usersurveyassessmentrepository)
        {

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _usersurveyassessmentrepository = usersurveyassessmentrepository ?? throw new ArgumentNullException(nameof(usersurveyassessmentrepository));
        }
        public async Task<List<UserSurveyAssessmentSurveyVm>> Handle(GetUserSurveyAssessmentByIdsQuery request, CancellationToken cancellationToken)
        {
            List<long> ids = new List<long>();
            foreach(var item in request.UserSurvey)
            {
                long id = 0;
                id = item.Id;
                ids.Add(id);
            }
            var userList = await _usersurveyassessmentrepository.GetUserSurveyAssessmentList(ids);
            return _mapper.Map<List<UserSurveyAssessmentSurveyVm>>(userList);
        }
    }

    

}
