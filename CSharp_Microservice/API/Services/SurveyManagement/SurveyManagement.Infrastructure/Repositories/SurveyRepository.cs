
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
    public class SurveyRepository: RepositoryBase<Survey>, ISurveyRepository
    {
        public SurveyRepository(SurveyManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Survey>> GetSurvey()
        {
            //var RoleList = await _dbContext.Roles.ToListAsync();
            //return RoleList;
            var SurveyList = await _dbContext.Surveys
                                        //  .Include(s => s.SurveyRoleMappings)
                                          .ToListAsync();
            return SurveyList;
        }
        public async Task<long> AddSurvey(Survey survey)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Surveys.Add(survey);
                    var surveyres = await _dbContext.SaveChangesAsync();
                    survey.SurveyId = "SI" + survey.Id .ToString().PadLeft(survey.Id.ToString().Length + 5 - survey.Id.ToString().Length, '0');
                    var cm = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return survey.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;

                }
            }
            
        }

        public async Task<Survey> GetSurveyById(long id)
        {
            var surveys = await _dbContext.Surveys
                          //  .Include(x => x.SurveyRoleMappings)
                                .FirstOrDefaultAsync(a => a.Id == id);
            return surveys;

        }

        public async Task<IEnumerable<Survey>> GetSurveyByCurrentFinancialYear(string CurrentFYYear)
        {
            var surveys = await _dbContext.Surveys
                          //  .Include(x => x.SurveyRoleMappings)
                                .Where(a => a.ToPeriod.Year.ToString() == CurrentFYYear.ToString())
                                          //& a.ToPeriod.Year.ToString() == CurrentFYYear.ToString())
                                .ToListAsync();
            return surveys;

        }

        public async Task<Survey> GetSurveyOrderById(Expression<Func<Survey, bool>> predicate = null, Func<IQueryable<Survey>, IOrderedQueryable<Survey>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.Surveys.Any())
                {
                    return await (_dbContext.Surveys).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.Surveys).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.Surveys).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<Survey>> GetSurveyQuery(Expression<Func<Survey, bool>> predicate = null, Func<IQueryable<Survey>, IOrderedQueryable<Survey>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<Survey> query = _dbContext.Set<Survey>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<long> UpdateSurvey(Survey survey)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Surveys.Add(survey);
                    _dbContext.Entry(survey).State = EntityState.Modified;
                    var cg = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return survey.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
    }
}
