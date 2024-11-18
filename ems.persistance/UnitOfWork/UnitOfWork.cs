using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmsDbContext _context;
    private readonly ILoggerFactory _loggerFactory;

    private IEmployeeRepository? _employeeRepository;
    private IBasicMenuRepository? _basicMenuRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;
    private IUserRoleRepository? _userRoleRepository;
    private IRoleRepository? _roleRepository;
    private IEmployeeTypeRepository? _employeeTyperepository;

    public UnitOfWork(EmsDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _loggerFactory = loggerFactory;
    }

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

    public IEmployeeTypeRepository EmployeeTypeRepository
    {
        get
        {
            return _employeeTyperepository ??= new EmployeeTypeRepository(_context, _loggerFactory.CreateLogger<EmployeeTypeRepository>());
        }
    }


    


    public IUserRoleRepository UserRoleRepository
    {
        get
        {
            return _userRoleRepository ??= new UserRoleRepository(_context, _loggerFactory.CreateLogger < UserRoleRepository>());
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
            return _roleRepository ??= new RoleRepository(_context, _loggerFactory.CreateLogger<RoleRepository>());
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
    }
}
