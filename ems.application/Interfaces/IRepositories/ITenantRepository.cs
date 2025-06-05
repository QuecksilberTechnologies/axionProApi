using ems.application.DTOs.Registration;
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
        Task<Tenant> GetTenantByIdAsync(long id);
        Task<bool> CheckTenantByEmail(string email);
        Task<Tenant> GetByCodeAsync(string tenantCode);
        Task<List<Tenant>> GetAllTenantAsync();

        Task<long> AddTenantAsync(Tenant tenant);
        Task<long> AddTenantProfileAsync(TenantProfile tenantProfile);
        Task UpdateTenantAsync(Tenant tenant);
        Task DeleteTenantAsync(Tenant tenant);
        
    }
}
