using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Contracts.Persistence
{
    public interface IUserSurveyRepository : IAsyncRepository<UserSurvey>
    {
        Task<long> AddUserSurvey(UserSurvey survey);
        Task<UserSurvey> GetUserSurveyOrderById(Expression<Func<UserSurvey, bool>> predicate = null, Func<IQueryable<UserSurvey>, IOrderedQueryable<UserSurvey>> orderBy = null, string includeString = null, bool disableTracking = true);

        Task<UserSurvey> GetUserSurveyListBySurveyIdUserid(long id, long userid);

        Task<UserSurvey> GetUserSurveyById(long id);
        Task<IEnumerable<UserSurvey>> GetUserSurvey();
        Task<IEnumerable<AssessmentType>> GetAssessmentType();
        Task<AssessmentType> GetAssessmentTypeById(long id);
    }
}
