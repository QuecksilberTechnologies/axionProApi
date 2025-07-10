using ems.application.DTOs;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ems.persistance.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IDbContextFactory<WorkforceDbContext> _contextFactory;
        private readonly ILogger<ModuleRepository> _logger;

        public ModuleRepository(
            IDbContextFactory<WorkforceDbContext> contextFactory,
            ILogger<ModuleRepository> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }
        public async Task<Module?> GetModuleByIdAsync(long moduleId)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Modules.FirstOrDefaultAsync(m => m.Id == moduleId && m.IsActive == true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetModuleByIdAsync for ID {ModuleId}", moduleId);
                return null;
            }
        }

        public async Task<Module?> GetCommonMenuParentAsync()
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Modules
                    .FirstOrDefaultAsync(m => m.IsCommonMenu == true && m.IsModuleDisplayInUi == true && m.IsActive == true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching CommonMenu parent module.", ex);
            }
        }

        public async Task<List<ModuleDTO>> GetCommonMenuTreeAsync(int? parentId)
        {
            try
            {
                List<Module> allModules;

                // ✅ DbContext used only here
                await using (var context = await _contextFactory.CreateDbContextAsync())
                {
                    allModules = await context.Modules
                        .Where(m => m.IsActive && m.IsModuleDisplayInUi)
                        .OrderBy(m => m.Id)
                        .ToListAsync();
                }

                // ✅ Outside context — Safe recursion
                var result = BuildMenuTree(allModules, parentId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error in GetCommonMenuTreeAsync with ParentId={ParentId}", parentId);
                throw;
            }
        }


        private List<ModuleDTO> BuildMenuTree(List<Module> allModules, int? parentId)
        {
            return allModules
                .Where(m => m.ParentModuleId == parentId)
                .OrderBy(m => m.ItemPriority < 0 ? int.MaxValue : m.ItemPriority) // ✅ custom priority sort
                .ThenBy(m => m.ModuleName) // optional fallback
                .Select(m => new ModuleDTO
                {
                    Id = m.Id,
                    ModuleName = m.ModuleName,
                    SubModuleUrl = m.SubModuleUrl,
                    Path = m.Path,
                    DisplayName = m.DisplayName,                
                    ImageIconMobile = m.ImageIconMobile,
                    ImageIconWeb = m.ImageIconWeb,
                    Children = BuildMenuTree(allModules, m.Id)
                })
                .ToList();
        }

        public async Task<List<Module>> GetAllModulesAsync()
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Modules
                    .Where(m => m.IsActive == true)
                    .OrderBy(m => m.ModuleName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllModulesAsync");
                return new List<Module>();
            }
        }

        public async Task<Module> AddModuleAsync(Module module)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                module.AddedDateTime = DateTime.Now;
                module.IsActive = true;

                await context.Modules.AddAsync(module);
                await context.SaveChangesAsync();

                return module;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding module");
                throw;
            }
        }

        public async Task<Module> AddSubModuleAsync(Module module)
        {
            return await AddModuleAsync(module); // Same logic as AddModule
        }

        public async Task<bool> UpdateModuleAsync(Module module)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                var existing = await context.Modules.FindAsync(module.Id);
                if (existing == null) return false;

                existing.ModuleName = module.ModuleName;
                existing.ParentModuleId = module.ParentModuleId;
                existing.ImageIconWeb = module.ImageIconWeb;
                existing.ImageIconMobile = module.ImageIconMobile;
                existing.IsActive = module.IsActive;
                existing.UpdatedById = module.UpdatedById;
                existing.UpdatedDateTime = DateTime.Now;
                existing.Remark = module.Remark;

                context.Modules.Update(existing);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating module with ID {ModuleId}", module.Id);
                return false;
            }
        }

        public async Task<bool> DeleteModuleAsync(long moduleId)
        {
            try
            {
                await using var context = await _contextFactory.CreateDbContextAsync();

                var module = await context.Modules.FindAsync(moduleId);
                if (module == null) return false;

                module.IsActive = false;
                module.UpdatedDateTime = DateTime.Now;

                context.Modules.Update(module);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting module with ID {ModuleId}", moduleId);
                return false;
            }
        }
    }
}
