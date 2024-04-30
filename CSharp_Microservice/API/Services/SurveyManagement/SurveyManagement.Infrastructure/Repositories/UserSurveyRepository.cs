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
    public class UserSurveyRepository : RepositoryBase<UserSurvey>, IUserSurveyRepository
    {
        public UserSurveyRepository(SurveyManagementContext dbContext) : base(dbContext)
        {

        }
        public async Task<UserSurvey> GetUserSurveyOrderById(Expression<Func<UserSurvey, bool>> predicate = null, Func<IQueryable<UserSurvey>, IOrderedQueryable<UserSurvey>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.UserSurveys.Any())
                {
                    return await (_dbContext.UserSurveys).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.UserSurveys).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.UserSurveys).FirstOrDefaultAsync();
        }

        public async Task<long> AddUserSurvey(UserSurvey userSurvey)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //var ret = await _dbContext.AddAsync(userSurvey);
                    //_dbContext.SaveChanges();
                    //return ret.Entity.Id;

                    _dbContext.UserSurveys.Add(userSurvey);
                    var surveyres = await _dbContext.SaveChangesAsync();
                    userSurvey.AssessmentID = "AI" + userSurvey.Id.ToString().PadLeft(userSurvey.Id.ToString().Length + 5 - userSurvey.Id.ToString().Length, '0');
                    var us = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return userSurvey.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;

                }
            }
            
        }

        public async Task<UserSurvey> GetUserSurveyListBySurveyIdUserid(long id, long userid)
        {
            try
            {
                var UserSurvey = await _dbContext.UserSurveys.Include(x => x.UserSurveyAssessments)
                .FirstOrDefaultAsync(a => a.SurveyId == id && a.UserId == userid);
                return UserSurvey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserSurvey> GetUserSurveyById(long id)
        {
            try
            {
                var UserSurvey = await _dbContext.UserSurveys.Include(x => x.UserSurveyAssessments)
                .FirstOrDefaultAsync(a => a.Id == id);
                return UserSurvey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<UserSurvey>> GetUserSurvey()
        {
            try
            {
                var UserSurvey = await _dbContext.UserSurveys.Include(x => x.UserSurveyAssessments).ThenInclude(x=>x.AssessmentType)
                .ToListAsync();
                return UserSurvey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<AssessmentType>> GetAssessmentType()
        {
            try
            {
                var UserSurvey = await _dbContext.AssessmentTypes.ToListAsync();
                return UserSurvey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AssessmentType> GetAssessmentTypeById(long id)
        {
            try
            {
                var UserSurvey = await _dbContext.AssessmentTypes.FirstOrDefaultAsync(a => a.Id == id);
                return UserSurvey;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
