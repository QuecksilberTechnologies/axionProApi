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
    public class ModuleRepository : IModuleRepository
    {


        private WorkforceDbContext _context;
        private ILogger<ModuleRepository> _logger;

        public ModuleRepository(WorkforceDbContext context, ILogger<ModuleRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }
        public async Task<Module?> GetModuleByIdAsync(long moduleId)
        {
            try
            {
                return await _context.Modules.FirstOrDefaultAsync(m => m.Id == moduleId && m.IsActive == true);
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
                return await _context.Modules
                    .FirstOrDefaultAsync(m => m.IsCommonMenu == true && m.IsModuleDisplayInUi == true && m.IsActive ==true);
            }
            catch (Exception ex)
            {
                // ✅ Logging ya custom handling
                throw new Exception("Error fetching CommonMenu parent module.", ex);
            }
        }



        public async Task<List<Module>> GetCommonMenuTreeAsync(int? parentId)
        {
            try
            {
                var modules = await _context.Modules
                    .Where(m => m.ParentModuleId == parentId &&
                                m.IsActive == true &&
                                m.IsModuleDisplayInUi == true)
                    .OrderBy(m => m.Id)
                    .ToListAsync();

                var result = new List<Module>();

                foreach (var module in modules)
                {
                    var child = new Module
                    {
                        Id = module.Id,
                        ModuleName = module.ModuleName,
                        SubModuleUrl = module.SubModuleUrl,
                        ChildModules = await GetCommonMenuTreeAsync(module.Id)
                    };

                    result.Add(child);
                    _logger.LogInformation("Adding module: {Name}, ChildCount: {Count}", module.ModuleName, child.ChildModules?.Count);

                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCommonMenuTreeAsync with ParentId={ParentId}", parentId);
                throw new Exception($"Error while fetching common menu tree for ParentId: {parentId}", ex);
            }
        }



        public async Task<List<Module>> GetAllModulesAsync()
        {
            try
            {
                return await _context.Modules
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
                module.AddedDateTime = DateTime.Now;
                module.IsActive = true;

                await _context.Modules.AddAsync(module);
                await _context.SaveChangesAsync();

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
            try
            {
                module.AddedDateTime = DateTime.Now;
                module.IsActive = true;

                await _context.Modules.AddAsync(module);
                await _context.SaveChangesAsync();

                return module;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding module");
                throw;
            }
        }

        public async Task<bool> UpdateModuleAsync(Module module)
        {
            try
            {
                var existing = await _context.Modules.FindAsync(module.Id);
                if (existing == null)
                    return false;

                existing.ModuleName = module.ModuleName;
            //    existing.SubModuleURL = module.SubModuleURL;
                existing.ParentModuleId = module.ParentModuleId;
                existing.ImageIconWeb = module.ImageIconWeb;
                existing.ImageIconMobile = module.ImageIconMobile;
             //   existing.IsModuleDisplayInUI = module.IsModuleDisplayInUI;
                existing.IsActive = module.IsActive;
                existing.UpdatedById = module.UpdatedById;
                existing.UpdatedDateTime = DateTime.Now;
                existing.Remark = module.Remark;

                _context.Modules.Update(existing);
                await _context.SaveChangesAsync();

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
                var module = await _context.Modules.FindAsync(moduleId);
                if (module == null)
                    return false;

                module.IsActive = false;
                module.UpdatedDateTime = DateTime.Now;

                _context.Modules.Update(module);
                await _context.SaveChangesAsync();

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
