using ems.application.Interfaces.IRepositories;

namespace ems.application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IEmployeeRepository Employees { get; }
    ICandidateRegistrationRepository CandidatesRegistrationRepository { get; }
   // ICandidateCategorySkillRepository CandidateCategorySkillRepository { get; }
    IEmployeeTypeRepository EmployeeTypeRepository { get; }
    IUserLoginReopsitory UserLoginReopsitory { get; }
    IUserRoleRepository UserRoleRepository { get; }
    ICategoryRepository CategoryRepository { get; }
   // ITenderCategoryRespository TenderCategoryRepository { get; }
    IRoleRepository RoleRepository { get; }
    IEmployeeTypeBasicMenuRepository EmployeeTypeBasicMenuRepository { get; }
    IUserRolesPermissionOnModuleRepository UserRolesPermissionOnModuleRepository { get; }

    // Begin a transaction
    Task BeginTransactionAsync();

    // Commit a transaction
    Task CommitTransactionAsync();

    // Rollback a transaction
    Task RollbackTransactionAsync();

    // Save changes asynchronously
    Task<int> CommitAsync();
}

