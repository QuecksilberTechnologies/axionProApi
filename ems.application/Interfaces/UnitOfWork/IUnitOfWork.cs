using ems.application.Interfaces.Repositories;

namespace ems.application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    ICompanyRepository Companys { get; }
    int Complete();
}
