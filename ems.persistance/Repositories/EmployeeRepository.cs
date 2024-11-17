using ems.application.Interfaces.IRepositories;
using ems.domain.Entity.EmployeeModule;
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
        private EmsDbContext context;
        private ILogger _logger;

        public EmployeeRepository(EmsDbContext context)
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

        public async Task<Employee> GetEmployeeByIdAsync(long employeeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee details for ID: {EmployeeId}", employeeId);

                //var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
              //  var employee = await context.Employees.Include(e => e.EmployementType).Include(e => e.UserRolesEmp).ThenInclude(ur => ur.RolesUr)
                                  
                //    .FirstOrDefaultAsync(e => e.Id == employeeId);
                var employee = await context.Employees.Include(e => e.EmployementType).Include(e => e.UserRolesEmp)
                 .ThenInclude(ur => ur.RolesUr).FirstOrDefaultAsync(e => e.Id == employeeId);
                if (employee == null)
                {
                    _logger?.LogWarning("Employee not found with ID: {EmployeeId}", employeeId);
                    return null;
                }

                return employee;
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




 
 






