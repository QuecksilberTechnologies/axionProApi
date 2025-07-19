
using ems.application.DTOs.Common;
using ems.domain.Entity;
using MediatR;

namespace ems.application.Interfaces.IRepositories;

public interface IEmployeeRepository
{

  //  Task<bool> UpdateEmployeeFieldAsync(UpdateSingleFieldRequestDTO request);
    Task<Employee> GetEmployeeByIdAsync(long id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeInfoForLoginByIdAsync(long employeeId);
    Task<EmployeeBankDetail?> GetEmployeeBankInfoByIdAsync( long employeeId);
    Task<EmployeePersonalDetail?> GetEmployeePersonalInfoByIdAsync(long employeeId);
    Task<EmployeeExperience?> GetEmployeeExperienceInfoByIdAsync( long employeeId);
    Task<EmployeeEducation?> GetEmployeeEducationByIdAsync( long employeeId);

    // Task AddEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeFieldAsync(long employeeId, string entity, string fieldName, object? fieldValue, long updatedById);
   //  Task DeleteEmployeeAsync(int id);
   // Task AddAsync(Employee employeeEntity);
    Task<Employee> AddAsync(Employee entity);  // Ensure this returns Task<Employee>
    Task<long> AddEmployeeByAdminAsync(Employee entity, long addedId);  // Ensure this returns Task<Employee>
    Task<long> AddEmployeePersonalDetailByAdminAsync(long empId, long addedId);  // Ensure this returns Task<Employee>



    #region Bank-info
    Task<long> AddEmployeeBankInfoByAdminAsync(long employeeId,long addedId);  // Ensure this returns Task<Employee>

    #endregion



    #region Employee-dependent-info
    //    Task<long> AddEmployeeDependantInfoByAdminAsync(long employeeId);  // Ensure this returns Task<Employee>

    #endregion



    #region Employee-Education-info
       Task<long> AddEmployeeEducationInfoByPermittedUserAsync(long employeeId, long addedId);  // Ensure this returns Task<Employee>

    #endregion
  
    #region Employee-Education-info
    Task<long> AddEmployeeExperienceInfoByPermittedUserAsync(long employeeId, long addedId);

    #endregion



}
