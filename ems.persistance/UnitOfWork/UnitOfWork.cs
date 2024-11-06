using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.Extensions.Logging;
using FluentValidation;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmsDbContext _context;
    private readonly ILogger<UnitOfWork> _logger; // Add the logger here

    private IEmployeeRepository? _employeeRepository;
    private ICommonMenuRepository? _commonMenuRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;

    public UnitOfWork(EmsDbContext context)
    {
        this._context = context;
      //  this._logger = logger;
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
            return _employeeRepository ??= new EmployeeRepository(_context); // Pass the logger here
        }
    }

    public ICommonMenuRepository CommonMenuRepository
    {
        get
        {
            return _commonMenuRepository ??= new CommonMenuRepository(_context); // Pass the logger here as well
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
 