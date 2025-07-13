using ems.application.Common.Helpers;
using ems.application.Constants;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private WorkforceDbContext context;
        private ILogger _logger;

        public EmployeeRepository(WorkforceDbContext context)
        {
            this.context = context;
          //  this._logger = logger;  
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            // Entity ko DbSet mein add karte hain
            await context.AddAsync(entity);

            // Changes ko save karte hain database mein
            await context.SaveChangesAsync();

            // Added entity ko return karte hain
            return entity;
        }

        public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee?> GetEmployeeInfoForLoginByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee details for ID: {EmployeeId}", employeeId);

                var employee = await context.Employees
                  .Where(e =>    e.Id == employeeId  && e.IsActive == ConstantValues.IsByDefaultTrue &&
                  e.IsSoftDeleted == ConstantValues.IsByDefaultFalse)
                  .Select(e => new Employee
                  {
                      Id = e.Id,
                      TenantId = e.TenantId ?? 0L,  // 👈 Null fallback to 0L
                      DesignationId = e.DesignationId,
                      DepartmentId = e.DepartmentId,
                      EmployeeTypeId = e.EmployeeTypeId,
                      OfficialEmail = e.OfficialEmail,
                      FirstName = e.FirstName,
                      MiddleName = e.MiddleName,
                      LastName = e.LastName
                  })
                    .FirstOrDefaultAsync();

                return employee; // Sirf required fields return ho rahe hain!
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee details for ID: {EmployeeId}", employeeId);
                throw;
            }
        }


        public async Task<Employee?> GetEmployeeByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee details for ID: {EmployeeId}", employeeId);

                Employee? employee = await context.Employees
                    .Where(e => e.Id == employeeId && e.IsActive == true && e.IsSoftDeleted==false)
                    .FirstOrDefaultAsync();

                return employee; // पूरी entity return करें
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee details for ID: {EmployeeId}", employeeId);
                throw;
            }
        }

        public async Task<long> AddEmployeeByAdminAsync(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Employee entity cannot be null.");

            // Optional: Validate required properties
            if (string.IsNullOrWhiteSpace(entity.FirstName))
                throw new ArgumentException("First name is required.", nameof(entity.FirstName));

            if (string.IsNullOrWhiteSpace(entity.EmployementCode))
                throw new ArgumentException("Employment code is required.", nameof(entity.EmployementCode));

            try
            {
                // Add to DB
                await context.Employees.AddAsync(entity);
                await context.SaveChangesAsync();

                // Return generated EmployeeId
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding employee in AddEmployeeByAdminAsync.");
                throw new Exception("An error occurred while adding employee. Please try again later.", ex);
            }
        }
 
public async Task<bool> UpdateEmployeeFieldAsync(long employeeId, string fieldName, object? fieldValue, long updatedById)
    {
        try
        {
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && !e.IsSoftDeleted == true && !e.IsActive == false);

                if (employee == null)
                return false;

            // Get property (case-insensitive)
            var propertyInfo = typeof(Employee)
                .GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (propertyInfo == null || !propertyInfo.CanWrite)
                return false;

            // ✅ Safe conversion
            if (!TryConvertObjectToValue.TryConvertValue(fieldValue, propertyInfo.PropertyType, out var convertedValue))
            {
                Console.WriteLine($"[Conversion Failed] Field: {fieldName}, Value: {fieldValue}");
                return false;
            }

            // ✅ Set the value
            propertyInfo.SetValue(employee, convertedValue);

            // ✅ Audit trail
            employee.UpdatedById = updatedById;
            employee.UpdatedDateTime = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EXCEPTION] UpdateEmployeeFieldAsync: {ex.Message}");
            return false;
        }
    }


    // Method to fetch employee type by ID

}
}




 
 






