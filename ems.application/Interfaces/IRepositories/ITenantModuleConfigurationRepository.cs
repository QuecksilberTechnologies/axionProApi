using ems.application.DTOs.Module;
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
        Task  CreateDyDefaultEnabledModulesAsync(long tenantId, List<TenantEnabledModule> moduleEntities, List<TenantEnabledOperation> operationEntities);

        Task<List<TenantEnabledModule>> GetEnabledModulesWithOperationsAsync(long tenantId);

    }
}
