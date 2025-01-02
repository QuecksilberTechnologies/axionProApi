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
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
       
        private ILogger _logger;
        private WorkforceDbContext _context;
        private ILogger<EmployeeTypeRepository> logger;

      

        public EmployeeTypeRepository(WorkforceDbContext context, ILogger<EmployeeTypeRepository> logger)
        {
            this._context = context;
            this.logger = logger;
        }


        // Method to fetch employee type by ID
        public async Task<EmployeeType> GetEmployeeTypeByIdAsync(int? employeeTypeId)
        {
            try
            {
                _logger?.LogInformation("Fetching employee type for ID: {EmployeeTypeId}", employeeTypeId);

                var employeeType = await _context.EmployeeTypes
                                         .Include(et => et.Role)          // Include Role entity
                                         .Include(et => et.EmployeeTypeBasicMenus)    // Include CommonMenus collection
                                         .FirstOrDefaultAsync(et => et.Id == employeeTypeId);

                if (employeeType == null)
                {
                    _logger?.LogWarning("Employee type not found with ID: {EmployeeTypeId}", employeeTypeId);
                    return null;
                }

                return employeeType;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching employee type for ID: {EmployeeTypeId}", employeeTypeId);
                throw;
          
            }
        }

        Task<EmployeeType> IEmployeeTypeRepository.GetEmployeeTypeByIdAsync(int? employeeTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
