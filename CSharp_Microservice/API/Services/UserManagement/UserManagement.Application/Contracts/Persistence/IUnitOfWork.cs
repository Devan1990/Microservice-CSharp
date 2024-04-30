using UserManagement.Application.Contracts.Persistence;

namespace CompetencyFramework.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
    }
}