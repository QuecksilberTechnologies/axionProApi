using ems.application.DTOs.Module;
using ems.application.DTOs.ModuleOperation;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IModuleOperationMappingRepository
    {

       
        // Save new mappings
        Task<ModuleOperationMappingResponseDTO> SaveModuleOperationMappingsAsync(ModuleOperationMappingRequestDTO dto);
        Task<ModuleOperationMapping> UpdateModuleOperationMappingsAsync(ModuleOperationMapping dto);
            
            // Get mappings for a specific product owner/module
            Task<List<ModuleOperationMapping>> GetModuleOperationMappingsByProductOwnerAsync(int productOwnerId, int moduleId);
           Task<ModuleOperationMappingByProductOwnerResponseDTO?> GetModuleOperationMappingsByIdAsync(int id, int moduleId);

            // Optional: Delete or overwrite previous mappings


    }
}
