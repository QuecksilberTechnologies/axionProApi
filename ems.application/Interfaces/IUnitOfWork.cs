using ems.application.Interfaces.IRepositories;

namespace ems.application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }

    // ICompanyRepository Companys { get; }

    int Complete();

}
