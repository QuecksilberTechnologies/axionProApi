

using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.IRepositories;
 
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class EmployeeTypeBasicMenuRepository : IEmployeeTypeBasicMenuRepository
    {
        private readonly WorkforceDbContext? _context;
        private readonly ILogger? _logger;
        public EmployeeTypeBasicMenuRepository(WorkforceDbContext? context, ILogger<EmployeeTypeBasicMenuRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<BasicMenuDTO>> GetModuleListAndOperation(int? employeeTypeId, int? forPlatform)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<BasicMenuDTO>> IEmployeeTypeBasicMenuRepository.GetBasicMenuDTO(int? employeeTypeId, int? forPlatform)
        {
            try
            {
                // Log the start of fetching process
                _logger?.LogInformation("Fetching basic menu for EmployeeTypeId: {EmployeeTypeId}, Platform: {ForPlatform}", employeeTypeId, forPlatform);
                var menus = await _context.EmployeeTypeBasicsMenus
       .Where(menu =>
           menu.HasAccess == true &&                   // Only menus with access
           menu.IsMenuDisplayInUi == true &&           // Only menus set to display in UI
           menu.IsActive == true &&                    // Only active menus
           menu.ForPlatform == forPlatform &&          // Filter by platform (Web/Mobile)
           (employeeTypeId == null || menu.EmployeeTypeId == employeeTypeId)) // Filter by EmployeeTypeId if provided
          .Include(menu => menu.BasicMenu) // Eager load the BasicMenu entity
           .ThenInclude(basicMenu => basicMenu.ParentMenu) // Eager load the ParentMenu (related to BasicMenu)
       .Select(menu => new BasicMenuDTO
       {
           Id = menu.Id,
           MenuName = menu.BasicMenu.MenuName,
           MenuUrl = menu.BasicMenu.MenuUrl,
           ParentMenuId = menu.BasicMenu.ParentMenuId,
           ParentMenuName = menu.BasicMenu.ParentMenu != null ? menu.BasicMenu.ParentMenu.MenuName : null, // Get ParentMenuName
           ForPlatform = menu.ForPlatform,
           ImageIcon = menu.BasicMenu.ImageIcon,
           IsMenuDisplayInUi = menu.IsMenuDisplayInUi,
           IsDisplayable = menu.IsDisplayable
       })
       .ToListAsync();


                var tttt = menus;

                // Organize into hierarchical structure (optional based on requirement)


                // Log success
                _logger?.LogInformation("Basic menu fetched successfully for EmployeeTypeId: {EmployeeTypeId}, Platform: {ForPlatform}", employeeTypeId, forPlatform);

                return menus;
            }
            catch (Exception ex)
            {
                // Log error
                _logger?.LogError(ex, "An error occurred while fetching the basic menu for EmployeeTypeId: {EmployeeTypeId}, Platform: {ForPlatform}", employeeTypeId, forPlatform);

                // Re-throw with user-friendly message
                throw new Exception("Failed to fetch basic menu. Please try again later.", ex);
            }
        }
    }

}
