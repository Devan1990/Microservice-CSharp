
using Microsoft.EntityFrameworkCore;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Domain.Entities;
using SurveyManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyManagement.Infrastructure.Repositories
{
    public class UserSurveyAssessmentRepository : RepositoryBase<UserSurveyAssessment>, IUserSurveyAssessmentRepository
    {
        public UserSurveyAssessmentRepository(SurveyManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<UserSurveyAssessment>> GetUserSurveyQuery(Expression<Func<UserSurveyAssessment, bool>> predicate = null, Func<IQueryable<UserSurveyAssessment>, IOrderedQueryable<UserSurveyAssessment>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<UserSurveyAssessment> query = _dbContext.Set<UserSurveyAssessment>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<UserSurveyAssessment> GetUserSurveyAssessmentByIdSurveyRoleid(long usersurveyid,long assorroleid)
        {
            var userSurveyAssessment = await _dbContext.UserSurveyAssessments
                                .Where(a => a.UserSurvey.Id == usersurveyid && a.AssessorRoleId== assorroleid).FirstOrDefaultAsync();
            return userSurveyAssessment;
        }

        public async Task<UserSurveyAssessment> GetUserSurveyAssessmentById(long Id)
        {
            var userSurveyAssessment = await _dbContext.UserSurveyAssessments.Include(a => a.AssessmentType).Include(a => a.UserSurvey)
                               .FirstOrDefaultAsync(a => a.Id == Id);
            return userSurveyAssessment;
        }

        public async Task<IEnumerable<UserSurveyAssessment>> GetUserSurveyAssessmentList(List<long> ids)
        {
            try
            {
                var UserSurvey = await _dbContext.UserSurveyAssessments.Include(a => a.AssessmentType).Include(a=>a.UserSurvey)
                                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
                return UserSurvey;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
