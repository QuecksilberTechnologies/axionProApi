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
    public class TenantRepository : ITenantRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;
        public TenantRepository(WorkforceDbContext? context, ILogger<TenantRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }

       
        public async Task<List<Tenant>> GetAllTenantAsync() => await _context.Tenants.ToListAsync();
        public async Task<long> AddTenantAsync(Tenant tenant)
        {
            try
            {
                if (_context == null)
                {
                    _logger?.LogError("DbContext is null in AddAsync.");
                    throw new ArgumentNullException(nameof(_context), "DbContext is not initialized.");
                }

                await _context.Tenants.AddAsync(tenant); // `Tenants` is DbSet<Tenant>
                await _context.SaveChangesAsync(); // Save changes to DB

                _logger?.LogInformation("Tenant created successfully with ID: {TenantId}", tenant.Id);

                return tenant.Id; // Tenant ID generated after SaveChanges
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while adding tenant.");
                throw; // Re-throw for handler to rollback transaction
            }
        }

        public async Task<long> AddTenantProfileAsync(TenantProfile tenantProfile)
        {
            try
            {
                tenantProfile.Id = 0;
                if (_context == null)
                {
                    _logger?.LogError("DbContext is null in AddAsync.");
                    throw new ArgumentNullException(nameof(_context), "DbContext is not initialized.");
                }

                await _context.TenantProfiles.AddAsync(tenantProfile); // `Tenants` is DbSet<Tenant>
                await _context.SaveChangesAsync(); // Save changes to DB

                _logger?.LogInformation("TenantProfile created successfully with ID: {TenantId}", tenantProfile.Id);

                return tenantProfile.Id; // Tenant ID generated after SaveChanges
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while adding tenant.");
                throw; // Re-throw for handler to rollback transaction
            }
        }

        public async Task<bool> CheckTenantByEmail(string email)
        {
            try
            {
                return await Task.FromResult(_context.Tenants.Any(t => t.TenantEmail == email));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking tenant email existence.");
                throw;
            }
        }

        public Task DeleteTenantAsync(Tenant tenant)
        {
            throw new NotImplementedException();
        }

      

        public Task<Tenant> GetByCodeAsync(string tenantCode)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant> GetTenantByIdAsync(long id)
        {
            throw new NotImplementedException();
        }




        public async Task<Tenant> UpdateTenantAsync(Tenant tenant)
        {
            try
            {
                var existingTenant = await _context.Tenants
                    .FirstOrDefaultAsync(x => x.Id == tenant.Id);

                if (existingTenant == null)
                {
                    return null; // Tenant not found
                }

                existingTenant.IsActive = true;
                existingTenant.IsVerified = true;

                _context.Tenants.Update(existingTenant);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    // Update successful
                    return existingTenant;
                }
                else
                {
                    // Nothing updated (maybe values were already true)
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Optional: Agar aapke paas ILogger<TenantRepository> injected hai to use yahan log kar sakte ho
                Console.WriteLine($"Error while updating tenant: {ex.Message}");

                // Fail-safe: return null ya custom exception throw kar sakte ho
                return null;
            }
        }



    }
}
