using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface ITenantRepository
    {
        Task<Tenant> GetByIdAsync(long id);
        Task<Tenant> GetByCodeAsync(string tenantCode);
        Task<IEnumerable<Tenant>> GetAllAsync();
        Task AddAsync(Tenant tenant);
        Task UpdateAsync(Tenant tenant);
        Task DeleteAsync(Tenant tenant);
        Task<bool> IsTenantEmailExistsAsync(string email);
    }
}
