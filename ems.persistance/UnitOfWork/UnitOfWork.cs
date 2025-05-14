using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ems.application.Interfaces.ITokenService;
public class UnitOfWork  : IUnitOfWork
{
    private readonly WorkforceDbContext _context;
    private readonly ILoggerFactory _loggerFactory;
    private IDbContextTransaction? _currentTransaction;
    private ILeaveRepository? _leaveRepository;
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
    private IAssetRepository? _assetRepository;
    //private  INewTokenRepository _tokenService;
    private IRefreshTokenRepository _refreshTokenRepository;
    private ICommonRepository _commonRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;


    // private IClientRepository? _clientRepository;


    public UnitOfWork(WorkforceDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
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

    public IEmployeeTypeBasicMenuRepository EmployeeTypeBasicMenuRepository
    {
        get
        {
            return _employeeTypeBasicMenurepository ??= new EmployeeTypeBasicMenuRepository(_context, _loggerFactory.CreateLogger<EmployeeTypeBasicMenuRepository>());
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

    public IAssetRepository AssetRepository => new  AssetRepository(_context, _loggerFactory.CreateLogger<AssetRepository>());
    public ITravelRepository TravelRepository => new TravelRepository(_context, _loggerFactory.CreateLogger<TravelRepository>());

    public IOperationRepository OperationRepository => new OperationRepository(_context, _loggerFactory.CreateLogger<OperationRepository>());
    public IDesignationRepository DesignationRepository => new DesignationRepository(_context, _loggerFactory.CreateLogger<DesignationRepository>());


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
