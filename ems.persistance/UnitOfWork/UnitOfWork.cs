using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;

namespace ems.persistance.UnitOfWork;

public class UnitOfWork(EmsDbContext context) : IUnitOfWork
{
    private readonly EmsDbContext _context = context;
    private IEmployeeRepository? _employeeRepository;
    private IUserLoginReopsitory? _userLoginReopsitory;

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
