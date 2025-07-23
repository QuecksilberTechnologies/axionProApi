using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IDepartmentRepository
    {
       

            Task<Department?> GetByIdAsync(long id);
            Task<List<Department>> GetAllByTenantAsync(long tenantId);
            Task<List<Department>> GetAllActiveAsync(long? tenantId);
            Task<long> CreateAsync(Department department);
             Task<int> AutoCreateDepartmentSeedAsync(List<Department>? departments);

            Task<bool> UpdateAsync(Department department);
            Task<bool> SoftDeleteAsync(long id, long deletedById);
            Task<bool> ExistsAsync(long id, long tenantId);
           Task<Dictionary<string, int>> GetDepartmentNameIdMapAsync(long tenantId);

    }
}
