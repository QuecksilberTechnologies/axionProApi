using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
public class UnitOfWork  : IUnitOfWork
{
    private readonly WorkforceDbContext _context;
    private readonly ILoggerFactory _loggerFactory;
    private IDbContextTransaction? _currentTransaction;

    private IEmployeeRepository? _employeeRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;
    private IUserRoleRepository? _userRoleRepository;
    private IRoleRepository? _roleRepository;
    private IEmployeeTypeRepository? _employeeTyperepository;
    private ICategoryRepository? _categoryRepository;
    private IEmployeeTypeBasicMenuRepository? _employeeTypeBasicMenurepository;
    private IUserRolesPermissionOnModuleRepository? _userRolesPermissionOnModuleRepository;
    private IAttendanceRepository? _attendanceRepository;
    private ICandidateRegistrationRepository? _candidateRegistrationRepository;
   // private ICandidateCategorySkillRepository? _candidateCategorySkillRepository;


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
            return _userLoginReopsitory ??= new UserLoginReopsitory(_context);
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

    //public ICandidateCategorySkillRepository CandidateCategorySkillRepository
    //{
    //    get
    //    {
    //        return _candidateCategorySkillRepository ??= new CandidateCategorySkillRepository(_context, _loggerFactory.CreateLogger<CandidateCategorySkillRepository>());
    //    }
    //}

    public IEmployeeTypeRepository EmployeeTypeRepository
    {
        get
        {
            return _employeeTyperepository ??= new EmployeeTypeRepository(_context, _loggerFactory.CreateLogger<EmployeeTypeRepository>());
        }
    }

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
        }
    }

    //public ICandidateCategorySkillRepository CandidateCategorySkillRepository => throw new NotImplementedException();

    //public ITenderCategoryRespository TenderCategoryRepository => throw new NotImplementedException();


    // Transaction Management
    public async Task BeginTransactionAsync()
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _currentTransaction?.CommitAsync();
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
        _context.Dispose();
        _currentTransaction?.Dispose();
    }
}
