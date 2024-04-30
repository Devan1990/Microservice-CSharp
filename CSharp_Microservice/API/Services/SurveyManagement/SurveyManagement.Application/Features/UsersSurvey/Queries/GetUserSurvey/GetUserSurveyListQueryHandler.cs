using AutoMapper;
using MediatR;
using SurveyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey
{
    public class GetUserSurveyListQueryHandler : IRequestHandler<GetUserSurveyListQuery, List<UserSurveyVm>>
    {
        private readonly IUserSurveyRepository _usersurveyrepository;
        private readonly IMapper _mapper;
        public GetUserSurveyListQueryHandler(IUserSurveyRepository usersurveyrepository, IMapper mapper)
        {
            _usersurveyrepository = usersurveyrepository ?? throw new ArgumentNullException(nameof(usersurveyrepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<UserSurveyVm>> Handle(GetUserSurveyListQuery request, CancellationToken cancellationToken)
        {
            var userList = await _usersurveyrepository.GetUserSurvey();
            return _mapper.Map<List<UserSurveyVm>>(userList);
        }
    }
}
