using SurveyManagement.Application.Contracts.Persistence;

namespace SurveyManagement.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        ISurveyRepository SurveyRepository { get; }
        
    }
}