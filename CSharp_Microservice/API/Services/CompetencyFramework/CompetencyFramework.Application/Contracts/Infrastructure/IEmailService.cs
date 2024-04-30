using CompetencyFramework.Application.Models;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
