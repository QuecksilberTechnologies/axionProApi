using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.domain.Entity;

namespace ems.application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IModuleRepository ModuleRepository { get; }
    IUserLoginReopsitory UserLoginRepository { get; }

    ITenantModuleConfigurationRepository TenantModuleConfigurationRepository { get; }
    
   
    ICommonRepository CommonRepository { get; }
    ICountryRepository CountryRepository { get; }
    //
    // INewTokenRepository newTokenRepository { get; } 

    ISubscriptionRepository SubscriptionRepository { get; }
    IPlanModuleMappingRepository PlanModuleMappingRepository { get; }

     ITenantRepository TenantRepository { get; }
    ITenantSubscriptionRepository TenantSubscriptionRepository { get; }
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
    IEmailTemplateRepository EmailTemplateRepository { get; }
 
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

