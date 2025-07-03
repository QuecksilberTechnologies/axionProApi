using ems.application.Constants;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Method to fetch employee type by ID

    }
}




 
 






