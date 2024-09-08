using ems.application.Interfaces.Repositories;
using ems.application.Interfaces.UnitOfWork;
using ems.persistance.Repositories;
using Persistance.Context;

namespace ems.persistance.UnitOfWork;

public class UnitOfWork(EmsDbContext context) : IUnitOfWork
{
    private readonly EmsDbContext _context = context;
    private IEmployeeRepository? _employeeRepository;
    private ICompanyRepository _companyRepository;
    public IEmployeeRepository Employees
    {
        get
        {
            return _employeeRepository ??= new EmployeeRepository(_context);
        }
    }

    public ICompanyRepository Companys
    {
        get
        {
            return _companyRepository ??= new CompanyRepository(_context);
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
