using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;

namespace ems.application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IUserLoginReopsitory UserLoginRepository { get; }
    ICommonRepository CommonRepository { get; }
   //
   // INewTokenRepository newTokenRepository { get; } 
 
    IRefreshTokenRepository RefreshTokenRepository { get; }
   // IEmployeeRepository EmployeeRepository { get; }
    IAssetRepository AssetRepository { get; }
    IOperationRepository OperationRepository { get; }
    IDesignationRepository  DesignationRepository { get; }
    IEmployeeRepository Employees { get; }
    ITravelRepository TravelRepository { get; }
    IClientRepository ClientsRepository { get; }
    ICandidateRegistrationRepository CandidatesRegistrationRepository { get; }
    ICandidateCategorySkillRepository CandidateCategorySkillRepository { get; }
    IEmployeeTypeRepository EmployeeTypeRepository { get; }
 
    IUserRoleRepository UserRoleRepository { get; }
    ICategoryRepository CategoryRepository { get; }
   // ITenderCategoryRespository TenderCategoryRepository { get; }
    IRoleRepository RoleRepository { get; }
    ILeaveRepository LeaveRepository { get; }
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

