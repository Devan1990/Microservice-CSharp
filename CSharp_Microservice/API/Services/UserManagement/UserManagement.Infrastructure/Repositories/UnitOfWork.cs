using CompetencyFramework.Application.Contracts.Persistence;
using UserManagement.Application.Contracts.Persistence;

namespace UserManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRoleRepository roleRepository
            , IUserRepository userRepository)
        {
            RoleRepository = roleRepository;
            UserRepository = userRepository;
        }

        public IRoleRepository RoleRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
