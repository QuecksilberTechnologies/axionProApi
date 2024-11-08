using ems.application.Interfaces.IRepositories;

namespace ems.application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IEmployeeRepository Employees { get; }
    IUserLoginReopsitory UserLoginReopsitory { get; }   
    ICommonMenuRepository CommonMenuRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
   
    IRoleRepository RoleRepository {  get; }
    // Aap apne aur repositories ko yahan add kar sakte hain
    // ICompanyRepository Companies { get; }

    // Save changes asynchronously
    Task<int> CommitAsync();
}
