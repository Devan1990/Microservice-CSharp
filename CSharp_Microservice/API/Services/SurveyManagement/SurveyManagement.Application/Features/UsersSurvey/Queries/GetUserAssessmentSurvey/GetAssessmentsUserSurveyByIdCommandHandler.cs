using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Features.UsersSurvey.Queries.GetUserSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Features.UsersSurvey.Queries.GetAssessmentSurvey
{
    public class GetAssessmentsUserSurveyByIdCommandHandler : IRequestHandler<GetAssessmentListByIdCommand, List<UserSurveyVm>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAssessmentListByIdCommand> _logger;
        private readonly IUserSurveyAssessmentRepository _usersurveyAssessmentrepository;
        public GetAssessmentsUserSurveyByIdCommandHandler( IMapper mapper, ILogger<GetAssessmentListByIdCommand> logger, IUserSurveyAssessmentRepository usersurveyAssessmentrepository)
        {

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersurveyAssessmentrepository = usersurveyAssessmentrepository ?? throw new ArgumentNullException(nameof(usersurveyAssessmentrepository));
        }

        public async Task<List<UserSurveyVm>> Handle(GetAssessmentListByIdCommand request, CancellationToken cancellationToken)
        {
            List<long> lst = new List<long>();
            var reqlist = request.usersurveyvm.ToList();
            foreach (var item in reqlist)
            {
                long id = 0;
                id = item.Id;
                lst.Add(id);
            }
            var userList = await _usersurveyAssessmentrepository.GetUserSurveyAssessmentList(lst);
            return _mapper.Map<List<UserSurveyVm>>(userList);
        }
    }
}
