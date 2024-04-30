using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
