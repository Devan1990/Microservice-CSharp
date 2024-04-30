using AutoMapper;
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
   
    public class AssessmentSurveyRepository  : RepositoryBase<AssessmentSurvey>, IAssessmentSurveyRepository
    {
        private readonly IUserSurveyAssessmentRepository _userSurveyassessmentRepository;
        private readonly IUserSurveyRepository _userSurveyRepository;
        private readonly IMapper _mapper;
        public AssessmentSurveyRepository(SurveyManagementContext dbContext, IUserSurveyAssessmentRepository userSurveyassessmentRepository, IUserSurveyRepository userSurveyRepository) : base(dbContext)
        {
            _userSurveyassessmentRepository = userSurveyassessmentRepository ?? throw new ArgumentNullException(nameof(userSurveyassessmentRepository));
            _userSurveyRepository = userSurveyRepository ?? throw new ArgumentNullException(nameof(userSurveyRepository));
        }
        public async Task<long> AddAssessmentSurvey(AssessmentSurvey assessmentSurvey,long assmenttypeid)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.AssessmentSurveys.Add(assessmentSurvey);
                    var roleres = await _dbContext.SaveChangesAsync();
                    assessmentSurvey.AssessmentSurveyId = "AS" + assessmentSurvey.Id.ToString().PadLeft(assessmentSurvey.Id.ToString().Length + 5 - assessmentSurvey.Id.ToString().Length, '0');
                    var rm = await _dbContext.SaveChangesAsync();
                    var assessmentsToUpdate = await _userSurveyassessmentRepository.GetByIdAsync(assessmentSurvey.UserSurveyAssessmentId);
                    if (assessmentsToUpdate.AssessorId != 0)
                    {
                        assessmentsToUpdate.AssessorId = assessmentsToUpdate.AssessorId;
                    }
                    var assessmentype = await _userSurveyRepository.GetAssessmentTypeById(assmenttypeid);
                    AssessmentType asstype = new AssessmentType();
                    asstype = assessmentype;
                    assessmentsToUpdate.AssessmentType = asstype;
                    await _userSurveyassessmentRepository.UpdateAsync(assessmentsToUpdate);
                    transaction.Commit();
                    return assessmentSurvey.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;

                }
            }
            //var ret = await _dbContext.AddAsync(assessmentSurvey);
            //return ret.Entity.Id;
        }
        public async Task<AssessmentSurvey> GetAssessmentSurveyOrderById(Expression<Func<AssessmentSurvey, bool>> predicate = null, Func<IQueryable<AssessmentSurvey>, IOrderedQueryable<AssessmentSurvey>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.AssessmentSurveys.Any())
                {
                    return await (_dbContext.AssessmentSurveys).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.AssessmentSurveys).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.AssessmentSurveys).FirstOrDefaultAsync();
        }

        public async Task<AssessmentSurvey> GetAssessmentSurveyById(long id)
        {
            var assessmentsurvey = await _dbContext.AssessmentSurveys
                      .FirstOrDefaultAsync(a => a.Id == id);
            return assessmentsurvey;
        }

        public async Task<IEnumerable<AssessmentSurvey>> GetSurvey()
        {
            var assessmentsurvey = await _dbContext.AssessmentSurveys
                      .ToListAsync();
            return assessmentsurvey;
        }

        public async Task<IEnumerable<AssessmentSurvey>> GetAssessmentSurveyByusersurveyId(long usersurveyid)
        {
            var assessmentsurvey = await _dbContext.AssessmentSurveys
                      .Where(a => a.UserSurveyAssessmentId == usersurveyid).ToListAsync();
            return assessmentsurvey;
        }
    }
}
