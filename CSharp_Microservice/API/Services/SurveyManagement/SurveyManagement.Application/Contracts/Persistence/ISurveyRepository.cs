using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SurveyManagement.Domain.Entities;
namespace SurveyManagement.Application.Contracts.Persistence
{
    public interface ISurveyRepository : IAsyncRepository<Survey>
    {
        Task<long> AddSurvey(Survey survey);
        Task<IEnumerable<Survey>> GetSurvey();
        Task<Survey> GetSurveyById(long id);
        Task<IEnumerable<Survey>> GetSurveyByCurrentFinancialYear(string CurrentFYYear);
        Task<Survey> GetSurveyOrderById(Expression<Func<Survey, bool>> predicate = null, Func<IQueryable<Survey>, IOrderedQueryable<Survey>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<IReadOnlyList<Survey>> GetSurveyQuery(Expression<Func<Survey, bool>> predicate = null, Func<IQueryable<Survey>, IOrderedQueryable<Survey>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<long> UpdateSurvey(Survey survey);
    }
}
