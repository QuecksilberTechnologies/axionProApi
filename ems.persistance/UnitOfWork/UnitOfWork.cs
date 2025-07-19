using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ems.application.Interfaces.ITokenService;
using System.Diagnostics.Metrics;
using ems.domain.Entity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using ems.application.Interfaces.Repositories;
public class UnitOfWork  : IUnitOfWork
{

    

    private readonly WorkforceDbContext _context;

    private readonly IDbContextFactory<WorkforceDbContext> _contextFactory;

    private readonly ILoggerFactory _loggerFactory;
    private IDbContextTransaction? _currentTransaction;
    private IForgotPasswordOtpRepository _forgotPasswordOtpRepository;
    private IDepartmentRepository _departmentRepository;

    private ITenantModuleConfigurationRepository? _tenantModuleConfigurationRepository;
    private IPlanModuleMappingRepository? _planModuleMappingRepository;
    private ILeaveRepository? _leaveRepository;
    private ITenantRepository? _tenantRepository;
    private IEmployeeRepository? _employeeRepository;
    private IUserRoleRepository? _userRoleRepository;
    private IRoleRepository? _roleRepository;
    private IEmployeeTypeRepository? _employeeTyperepository;
    private ICategoryRepository? _categoryRepository;
    private IEmployeeTypeBasicMenuRepository? _employeeTypeBasicMenurepository;
    private IUserRolesPermissionOnModuleRepository? _userRolesPermissionOnModuleRepository;
    private IAttendanceRepository? _attendanceRepository;
    private ICandidateRegistrationRepository? _candidateRegistrationRepository;
    private ICandidateCategorySkillRepository? _candidateCategorySkillRepository;
    private ICountryRepository? _countryRepository;

    private IAssetRepository? _assetRepository;
    //private  INewTokenRepository _tokenService;
    private IRefreshTokenRepository _refreshTokenRepository;
    private ICommonRepository _commonRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;
    private IEmailTemplateRepository _emailTemplateRepository;
    private ISubscriptionRepository? _subscriptionRepository;
    private ITenantSubscriptionRepository? _tenantSubscriptionRepository;
    private IModuleRepository? _moduleRepository;
    private IModuleOperationMappingRepository _moduleOperationMappingRepository; 
   
    // private IClientRepository? _clientRepository;



    //public UnitOfWork(WorkforceDbContext context, IMapper mapper, ILoggerFactory loggerFactory)
    //{
    //    _context = context;
    //   // _mapper = mapper;
    //    _loggerFactory = loggerFactory;
    //}
    public UnitOfWork(WorkforceDbContext context,
                  IDbContextFactory<WorkforceDbContext> contextFactory,
                  ILoggerFactory loggerFactory)
    {
        _context = context;
        _contextFactory = contextFactory;
        _loggerFactory = loggerFactory;
    }


    // Repositories
    //public IAttendanceRepository AttendanceReopsitory
    //{
    //    get
    //    {
    //        return _attendanceRepository ??= new AttendanceRepository(_context, _loggerFactory.CreateLogger<AttendanceRepository>());
    //    }
    //}




    public IUserLoginReopsitory UserLoginReopsitory
    {
        get
        {
            return _userLoginReopsitory ??= new UserLoginReopsitory(_context, _loggerFactory.CreateLogger<UserLoginReopsitory>());
        }
    }
    




    public IEmployeeRepository Employees
    {
        get
        {
            return _employeeRepository ??= new EmployeeRepository(_context);
        }
    }
   
    public IModuleOperationMappingRepository ModuleOperationMappingRepository
    {
        get
        {
            return _moduleOperationMappingRepository ??= new ModuleOperationMappingRepository(_context, _loggerFactory.CreateLogger<ModuleOperationMappingRepository>());
        }
    }
    public ITenantModuleConfigurationRepository TenantModuleConfigurationRepository
           {
                  get
                  {
                    return _tenantModuleConfigurationRepository ??= new TenantModuleConfigurationRepository(_context, _loggerFactory.CreateLogger<TenantModuleConfigurationRepository>());
                   }
            } 
          public ITenantRepository TenantRepository
           {
                  get
                  {
                    return _tenantRepository ??= new TenantRepository(_context, _loggerFactory.CreateLogger<TenantRepository>());
                   }
            }

    

           public IForgotPasswordOtpRepository ForgotPasswordOtpRepository
            {
                  get
                  {
            return _forgotPasswordOtpRepository ??= new ForgotPasswordOtpRepository(_context, _loggerFactory.CreateLogger<ForgotPasswordOtpRepository>());
               }
            }
               public IModuleRepository ModuleRepository
                  {
                      get
                        {
                       return _moduleRepository ??= new ModuleRepository(_contextFactory, _loggerFactory.CreateLogger<ModuleRepository>());
                       }
                 }


    public IEmployeeTypeBasicMenuRepository EmployeeTypeBasicMenuRepository
    {
        get
        {
            return _employeeTypeBasicMenurepository ??= new EmployeeTypeBasicMenuRepository(_context, _loggerFactory.CreateLogger<EmployeeTypeBasicMenuRepository>());
        }
    }

    public IPlanModuleMappingRepository PlanModuleMappingRepository
    {
        get
        {
            return _planModuleMappingRepository ??= new PlanModuleMappingRepository(_context, _loggerFactory.CreateLogger<PlanModuleMappingRepository>());
        }
    }



    public ICountryRepository CountryRepository
    {
        get
        {
            return _countryRepository ??= new CountryRepository(_context, _loggerFactory.CreateLogger<CountryRepository>());
        }
    }

    public IEmailTemplateRepository EmailTemplateRepository
    {
        get
        {
            return _emailTemplateRepository ??= new EmailTemplateRepository(_context, _loggerFactory.CreateLogger<EmailTemplateRepository>());
        }
    }

    public ICandidateRegistrationRepository CandidatesRegistrationRepository
    {
        get
        {
            return _candidateRegistrationRepository ??= new CandidateRegistrationRepository(_context, _loggerFactory.CreateLogger<CandidateRegistrationRepository>());
        }
    }

    public ICandidateCategorySkillRepository CandidateCategorySkillRepository
    {
        get
        {
            return _candidateCategorySkillRepository ??= new CandidateCategorySkillRepository(_context, _loggerFactory.CreateLogger<CandidateCategorySkillRepository>());
        }
    }


    public ITenantSubscriptionRepository TenantSubscriptionRepository
    {
        get
        {
            return _tenantSubscriptionRepository ??= new TenantSubscriptionRepository(_context, _loggerFactory.CreateLogger<TenantSubscriptionRepository>());
        }
    }
    public IEmployeeTypeRepository EmployeeTypeRepository
    {
        get
        {
            return _employeeTyperepository ??= new EmployeeTypeRepository(_context, _loggerFactory.CreateLogger<EmployeeTypeRepository>());
        }
    }
    public IClientRepository ClientsRepository => new ClientRepository(_context, _loggerFactory.CreateLogger<ClientRepository>());
    public ICommonRepository CommonRepository
    {
        get
        {
            return _commonRepository ??= new CommonRepository(_context, _loggerFactory.CreateLogger<CommonRepository>());
        }
    }
    //public INewTokenRepository NewTokenRepository
    //{
    //    get
    //    {
    //        return _tokenService ??= new NewTokenRepository(_context, _loggerFactory.CreateLogger<NewTokenRepository>());
    //    }
    //}
    //private INewTokenRepository _tokenService;
    //private IRefreshTokenRepository _refreshTokenRepository;
    //private ICommonRepository _commonRepository;
    //private IUserLoginReopsitory? _userLoginReopsitory;
    //public ICommonRepository CommonRepository => new CommonRepository(_context, _loggerFactory.CreateLogger<CommonRepository>());

    public IUserLoginReopsitory UserLoginRepository => new UserLoginReopsitory(_context, _loggerFactory.CreateLogger<UserLoginReopsitory>());
    
    
    public IRefreshTokenRepository RefreshTokenRepository =>  new RefreshTokenRepository(_context, _loggerFactory.CreateLogger<RefreshTokenRepository>());
    public IAssetRepository AssetRepository => new AssetRepository(_context, _loggerFactory.CreateLogger<AssetRepository>());
    public ISubscriptionRepository SubscriptionRepository => new SubscriptionRepository(_context, _loggerFactory.CreateLogger<SubscriptionRepository>());





    public ITravelRepository TravelRepository => new TravelRepository(_context, _loggerFactory.CreateLogger<TravelRepository>());

    public IOperationRepository OperationRepository => new OperationRepository(_context, _loggerFactory.CreateLogger<OperationRepository>());
    public IDesignationRepository DesignationRepository => new DesignationRepository(_context, _loggerFactory.CreateLogger<DesignationRepository>());
    public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_context, _loggerFactory.CreateLogger<DepartmentRepository>());


    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_context, _loggerFactory.CreateLogger<CategoryRepository>());
        }
    }

    public IUserRoleRepository UserRoleRepository
    {
        get
        {
            return _userRoleRepository ??= new UserRoleRepository(_context, _loggerFactory.CreateLogger<UserRoleRepository>());
        }
    }

    public IUserRolesPermissionOnModuleRepository UserRolesPermissionOnModuleRepository
    {
        get
        {
            return _userRolesPermissionOnModuleRepository ??= new UserRolesPermissionOnModuleRepository(_context, _loggerFactory.CreateLogger<UserRolesPermissionOnModuleRepository>());
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
               return _roleRepository ??= new RoleRepository(_context, _loggerFactory.CreateLogger<RoleRepository>());
              //   return _roleRepository ??= new CandidateCategorySkillRepository(_context, _loggerFactory.CreateLogger<RoleRepository>());

        }
    }

    public ILeaveRepository LeaveRepository
    {
        get
        {
            return _leaveRepository ??= new LeaveRepository(_context, _loggerFactory.CreateLogger<LeaveRepository>());
            

        }
    }





    //public ICandidateCategorySkillRepository CandidateCategorySkillRepository => throw new NotImplementedException();

    //public ITenderCategoryRespository TenderCategoryRepository => throw new NotImplementedException();


    // Transaction Management

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction == null) // Avoid nested transactions
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
        catch (Exception)
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }


}
