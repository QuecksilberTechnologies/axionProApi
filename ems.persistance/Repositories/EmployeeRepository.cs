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

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
 
    }
}




 
 






