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
    private ICommonMenuRepository? _commonMenuRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;
    private IUserRoleRepository? _userRoleRepository;
    private IRoleRepository? _roleRepository;

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

    public ICommonMenuRepository CommonMenuRepository
    {
        get
        {
            return _commonMenuRepository ??= new CommonMenuRepository(_context, _loggerFactory.CreateLogger<CommonMenuRepository>());
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
