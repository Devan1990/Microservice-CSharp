
using SurveyManagement.Application.Contracts.Persistence;


namespace SurveyManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ISurveyRepository surveyRepository)
        {
            SurveyRepository = surveyRepository;
            
        }

        public ISurveyRepository SurveyRepository { get; }
        
    }
}
