using ems.application.DTOs.Module;
using ems.application.DTOs.Tenant;
using ems.application.Wrappers;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
   public interface  ITenantModuleConfigurationRepository
    {
        Task  CreateByDefaultEnabledModulesAsync(long tenantId, List<TenantEnabledModule> moduleEntities, List<TenantEnabledOperation> operationEntities);
        Task<List<TenantEnabledModule>> GetTenantEnabledModulesWithOperationsAsync(long tenantId);
        Task <TenantEnabledModuleOperationsResponseDTO> GetEnabledModulesWithOperationsAsync(TenantEnabledModuleOperationsRequestDTO tenantEnabledModuleOperationsRequestDTO);
        /// <summary>
        /// Updates the module and operation enable/disable state for a given tenant.
        /// </summary>
        /// <param name="request">The request DTO containing module and operation status.</param>
        /// <returns>True if update successful, otherwise false.</returns>
        Task<bool> UpdateTenantModuleAndOperationsAsync(TenantModuleOperationsUpdateRequestDTO request);


    }
}
