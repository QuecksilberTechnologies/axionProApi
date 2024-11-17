 
using ems.domain.Entity.EmployeeModule;

namespace ems.application.Interfaces.IRepositories;

public interface IEmployeeRepository
{
     Task<Employee> GetEmployeeByIdAsync(long id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();

   // Task AddEmployeeAsync(Employee employee);
  //  Task UpdateEmployeeAsync(Employee employee);
  //  Task DeleteEmployeeAsync(int id);
   // Task AddAsync(Employee employeeEntity);
    Task<Employee> AddAsync(Employee entity);  // Ensure this returns Task<Employee>
    
    
}
