using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IModuleRepository
    {
        /// <summary>
        /// Ek module ko fetch karta hai by Id
        /// </summary>
        /// 

        Task<Module?> GetCommonMenuParentAsync();
        Task<List<ModuleDTO>> GetCommonMenuTreeAsync(int? parentModuleId);

        Task<Module?> GetModuleByIdAsync(long moduleId);

        /// <summary>
        /// Sare modules laata hai (optionally filterable)
        /// </summary>
        Task<List<Module>> GetAllModulesAsync();

        /// <summary>
        /// Naya module insert karta hai
        /// </summary>
        Task<Module> AddModuleAsync(Module module);
        Task<Module> AddSubModuleAsync(Module module);

        /// <summary>
        /// Module ko update karta hai
        /// </summary>
        Task<bool> UpdateModuleAsync(Module module);

        /// <summary>
        /// Module ko soft/hard delete karta hai
        /// </summary>
        Task<bool> DeleteModuleAsync(long moduleId);
    }
}
