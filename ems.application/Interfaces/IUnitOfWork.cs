using ems.application.Interfaces.IRepositories;

namespace ems.application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IEmployeeRepository Employees { get; }
   
    IEmployeeTypeRepository  EmployeeTypeRepository { get; }
    IUserLoginReopsitory UserLoginReopsitory { get; }   
  //  IBasicMenuRepository CommonMenuRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
   
    IRoleRepository RoleRepository {  get; }

    IEmployeeTypeBasicMenuRepository EmployeeTypeBasicMenuRepository { get; }
    IUserRolesPermissionOnModuleRepository UserRolesPermissionOnModuleRepository { get; }   
    // IAccessDetailRepository AccessDetailRepository {  get; }
    // Aap apne aur repositories ko yahan add kar sakte hain
    // ICompanyRepository Companies { get; }

    // Save changes asynchronously
    Task<int> CommitAsync();
}
