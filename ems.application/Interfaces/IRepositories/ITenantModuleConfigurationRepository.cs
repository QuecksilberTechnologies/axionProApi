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
        Task  CreateByDefaultEnabledModulesAsync(long? TenantId, List<TenantEnabledModule> moduleEntities, List<TenantEnabledOperation> operationEntities);
      //yeh function sirf enabled module or operation laata hai , login mei bhi used
        Task<List<TenantEnabledModule>> GetAllTenantEnabledModulesWithOperationsAsync(long? TenantId);
        //Task<List<TenantEnabledModule>> GetAllEnabledTrueModulesWithOperationsByTenantIdAsync(long? TenantId);
        Task <TenantEnabledModuleOperationsResponseDTO> GetAllEnabledModulesWithOperationsByTenantIdAsync(TenantEnabledModuleOperationsRequestDTO tenantEnabledModuleOperationsRequestDTO);
        Task <TenantEnabledModuleOperationsResponseDTO> GetAllEnabledTrueModulesWithOperationsByTenantIdAsync(TenantEnabledModuleOperationsRequestDTO tenantEnabledModuleOperationsRequestDTO);
        /// <summary>
        /// Updates the module and operation enable/disable state for a given tenant.
        /// </summary>
        /// <param name="request">The request DTO containing module and operation status.</param>
        /// <returns>True if update successful, otherwise false.</returns>
        Task<bool> UpdateTenantModuleAndItsOperationsAsync(TenantModuleOperationsUpdateRequestDTO request);

       
    }
}
