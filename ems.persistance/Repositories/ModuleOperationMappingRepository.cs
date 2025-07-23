using ems.application.DTOs.Module;
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
    public class ModuleOperationMappingRepository : IModuleOperationMappingRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<ModuleOperationMappingRepository> _logger;

        public ModuleOperationMappingRepository(WorkforceDbContext context, ILogger<ModuleOperationMappingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ModuleOperationMapping> UpdateModuleOperationMappingsAsync(ModuleOperationMapping mom)
        {
            try
            {
                if (mom == null || mom.Id <= 0)
                {
                    _logger.LogWarning("Invalid ModuleOperationMapping object passed to update.");
                    return null;
                }

                var existing = await _context.ModuleOperationMappings
                    .FirstOrDefaultAsync(x => x.Id == mom.Id && x.ModuleId == mom.ModuleId);

                if (existing == null)
                {
                    _logger.LogWarning("No existing ModuleOperationMapping found for Id {Id} and ModuleId {ModuleId}.", mom.Id, mom.ModuleId);
                    return null;
                }

                // 🔁 Update properties
               
                existing.PageUrl = mom.PageUrl;
                existing.IconUrl = mom.IconUrl;
                existing.IsCommonItem = mom.IsCommonItem;
                existing.IsOperational = mom.IsOperational;
                existing.Priority = mom.Priority;
                existing.Remark = mom.Remark;
                existing.IsActive = mom.IsActive;
                existing.UpdatedById = mom.UpdatedById;
                existing.UpdatedDateTime = mom.UpdatedDateTime ?? DateTime.Now;

                await _context.SaveChangesAsync();

                // ✅ Prepare and return response DTO
                

                _logger.LogInformation("ModuleOperationMapping updated successfully for Id {Id}", existing.Id);
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ModuleOperationMapping for Id {Id}", mom.Id);
                return null;
            }
        }

        public async Task<ModuleOperationMappingByProductOwnerResponseDTO> SaveModuleOperationMappingsAsync(ModuleOperationMappingByProductOwnerRequestDTO dto)
        {
            try
            {
                // 🔍 Basic Validation
                if (dto == null || dto.Operation == null || !dto.Operation.Any()
                    || dto.ModuleId <= 0 || dto.ProductOwnerId <= 0 || dto.ProductOwnerRoleId <= 0)
                {
                    _logger.LogWarning("Invalid mapping request. DTO or required fields are missing.");
                    return null;
                }

                // 🔁 Prepare list of entities to insert
                var mappings = dto.Operation.Select(op => new ModuleOperationMapping
                {
                  //  ProductOwnerId = dto.ProductOwnerId,
                   // ProductOwnerRoleId = dto.ProductOwnerRoleId,
                    ModuleId = dto.ModuleId,
                    OperationId = op.Id,
                    IsActive = true,
                    AddedById = dto.ProductOwnerId, // or dto.AddedById if present
                    AddedDateTime = DateTime.UtcNow
                }).ToList();

                if (!mappings.Any())
                {
                    _logger.LogWarning("No valid operation mappings to insert.");
                    return null;
                }

                await _context.ModuleOperationMappings.AddRangeAsync(mappings);
                int result = await _context.SaveChangesAsync();

                // ✅ Build and return a response DTO only if something was saved
                if (result > 0)
                {
                    return new ModuleOperationMappingByProductOwnerResponseDTO
                    {
                      //  ProductOwnerId = dto.ProductOwnerId,
                      //  ProductOwnerRoleId = dto.ProductOwnerRoleId,
                        ModuleId = dto.ModuleId,
                        OperationIds = dto.Operation.Select(o => o.Id).ToList(),
                     //   Remark = "Mappings saved successfully",
                        
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while saving ModuleOperationMapping.");
                return null;
            }
        }

        public async Task<ModuleOperationMappingByProductOwnerResponseDTO?> GetModuleOperationMappingsByIdAsync(int id, int moduleId)
        {
            try
            {
                var mapping = await _context.ModuleOperationMappings
                    .FirstOrDefaultAsync(x => x.Id == id && x.ModuleId == moduleId);

                if (mapping == null)
                {
                    _logger.LogWarning("No ModuleOperationMapping found for Id {Id} and ModuleId {ModuleId}", id, moduleId);
                    return null;
                }

                var responseDto = new ModuleOperationMappingByProductOwnerResponseDTO
                {
                    Id = mapping.Id,
                    ModuleId = mapping.ModuleId,
                    OperationIds = new List<int> { mapping.OperationId },
                     
                    PageURL = mapping.PageUrl,
                    IconURL = mapping.IconUrl,
                    IsCommonItem = mapping.IsCommonItem ?? false,
                    IsOperational = mapping.IsOperational ?? false,
                    Priority = mapping.Priority ?? 0,
                    Remark = mapping.Remark,
                    IsActive = mapping.IsActive,
                    AddedById = mapping.AddedById ?? 0,
                    AddedDateTime = mapping.AddedDateTime ?? DateTime.MinValue,
                    UpdatedById = mapping.UpdatedById,
                    UpdatedDateTime = mapping.UpdatedDateTime
                };

                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModuleOperationMapping for Id {Id} and ModuleId {ModuleId}", id, moduleId);
                return null;
            }
        }


        public async Task<List<ModuleOperationMapping>> GetModuleOperationMappingsByProductOwnerAsync(int productOwnerId, int moduleId)
        {
            try
            {
                //return await _context.ModuleOperationMappings
                //    .Where(x => x.ModuleId == productOwnerId && x.ModuleId == moduleId && x.IsActive)
                //    .ToListAsync();
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ModuleOperationMappings for ProductOwnerId {ProductOwnerId} and ModuleId {ModuleId}", productOwnerId, moduleId);
                return new List<ModuleOperationMapping>();
            }
        }
    }

}
