using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Contracts.Persistence
{
    public interface IUserSurveyAssessmentRepository : IAsyncRepository<UserSurveyAssessment>
    {
        Task<IReadOnlyList<UserSurveyAssessment>> GetUserSurveyQuery(Expression<Func<UserSurveyAssessment, bool>> predicate = null, Func<IQueryable<UserSurveyAssessment>, IOrderedQueryable<UserSurveyAssessment>> orderBy = null, string includeString = null, bool disableTracking = true);

        Task<UserSurveyAssessment> GetUserSurveyAssessmentByIdSurveyRoleid(long usersurveyid, long assorroleid);
        Task<UserSurveyAssessment> GetUserSurveyAssessmentById(long Id);
        Task<IEnumerable<UserSurveyAssessment>> GetUserSurveyAssessmentList(List<long> ids);
    }
}
