using System.Threading.Tasks;
using SurveyManagement.Application.Models;

namespace SurveyManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
