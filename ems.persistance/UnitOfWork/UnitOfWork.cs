using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;

namespace ems.persistance.UnitOfWork;

public class UnitOfWork(EmsDbContext context) : IUnitOfWork
{
    private readonly EmsDbContext _context = context;
    private IEmployeeRepository? _employeeRepository;
    
    public IEmployeeRepository Employees
    {
        get
        {
            return _employeeRepository ??= new EmployeeRepository(_context);
        }
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
