using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;
          public RoleRepository(WorkforceDbContext? context, ILogger<RoleRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }
       
            public async Task<Role> CreateRoleAsync(Role role)
            {
                try
                {
                    
                    await _context.Roles.AddAsync(role);
                    // Changes ko save karenge
                    await _context.SaveChangesAsync();

                    // Added role ko return karenge
                    return role;
                }
                catch (Exception ex)
                {
                    // Exception ko log karenge
                    _logger.LogError(ex, "Error occurred while creating role.");
                    throw;  // Rethrow the exception for further handling
                }
             


        }

        public Task<bool> DeleteRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            try
            {
                // Role ko Id ke basis par find karenge
                var role = await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync(); // Agar record milta hai toh return karega, nahi toh null

                if (role == null)
                {
                    _logger.LogWarning("Role with ID {RoleId} not found.", roleId);
                }

                return role;   
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                _logger.LogError(ex, "Error occurred while fetching role with ID {RoleId}.", roleId);
                throw;  // Rethrow the exception for further handling
            }
        }


        public Task<Role> UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
