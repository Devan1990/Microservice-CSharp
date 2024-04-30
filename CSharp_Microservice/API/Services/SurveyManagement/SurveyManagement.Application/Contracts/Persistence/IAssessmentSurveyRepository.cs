using SurveyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Application.Contracts.Persistence
{
    public interface IAssessmentSurveyRepository : IAsyncRepository<AssessmentSurvey>
    {
        Task<long> AddAssessmentSurvey(AssessmentSurvey assessmentsurvey, long assmenttypeid);
        Task<AssessmentSurvey> GetAssessmentSurveyOrderById(Expression<Func<AssessmentSurvey, bool>> predicate = null, Func<IQueryable<AssessmentSurvey>, IOrderedQueryable<AssessmentSurvey>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<AssessmentSurvey> GetAssessmentSurveyById(long id);
        Task<IEnumerable<AssessmentSurvey>> GetSurvey();

        Task<IEnumerable<AssessmentSurvey>> GetAssessmentSurveyByusersurveyId(long usersurveyid);
    }
}
