
using ems.domain.Entity;

namespace ems.application.Interfaces.IRepositories;

public interface IEmployeeRepository
{
     Task<Employee> GetEmployeeByIdAsync(long id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeInfoForLoginByIdAsync(long employeeId);
    // Task AddEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeFieldAsync(long employeeId, string fieldName, object? fieldValue, long updatedById);
   //  Task DeleteEmployeeAsync(int id);
   // Task AddAsync(Employee employeeEntity);
    Task<Employee> AddAsync(Employee entity);  // Ensure this returns Task<Employee>
    Task<long> AddEmployeeByAdminAsync(Employee entity);  // Ensure this returns Task<Employee>


}
