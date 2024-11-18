

using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity.EmployeeModule;
using ems.persistance.Data.Context;
using FluentValidation;
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
    public class AccessDetailRepository : IAccessDetailRepository
    {
        private readonly EmsDbContext? _context;
        private readonly ILogger? _logger;
        public AccessDetailRepository(EmsDbContext? context, ILogger<RoleRepository>? logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<AccessDetailResponseDTO> GetAccessDetailResponseDTO(AccessDetailRequestDTO accessDetailRequest)
        {
            try
            {
                // Validate context
                if (_context == null)
                    throw new Exception("Database context is null");

                // Build query for BasicMenuAccess
                var query = _context.ETBasicMenuAccess
                    .Where(bma => bma.HasAccess
                                  && bma.ForPlatform == (accessDetailRequest.ForPlatform ?? bma.ForPlatform)
                                  && (accessDetailRequest.IncludeInactive || bma.IsActive)
                                  && (!accessDetailRequest.ParentMenuId.HasValue || bma.BasicMenu.ParentMenuId == accessDetailRequest.ParentMenuId));

                // Optionally filter by Employee's Roles
                if (accessDetailRequest.roleInfo != null && accessDetailRequest.roleInfo.Any())
                {
                    var roleIds = accessDetailRequest.roleInfo.Select(r => r.Id).ToList();
                    query = query.Where(bma => roleIds.Contains(bma.EmployeeTypeId));
                }

                // Fetch data with necessary includes
                var result = await query
                    .Include(bma => bma.BasicMenu.ParentMenuId) // Include Menu Details
                    .Select(bma => new BasicMenuDTO
                    {
                        Id = bma.BasicMenu.ParentMenuId.Value,
                        MenuName = bma.BasicMenu.MenuName,
                        MenuUrl = bma.BasicMenu.MenuUrl,
                        ParentMenuId = bma.BasicMenu.ParentMenuId,                         
                        ImageIcon = bma.BasicMenu.ImageIcon,
                        IsMenuDisplayInUi = bma.IsMenuDisplayInUi,
                        IsDisplayable = bma.IsDisplayable,
                        IsSubMenu = bma.IsActive
                    })
                    .ToListAsync();

                // Map to response DTO
                return new AccessDetailResponseDTO
                {
                    EmployeeId = accessDetailRequest.EmployeeId,
                    ForPlatform = accessDetailRequest.ForPlatform,
                    Menus = result
                };
            }
            catch (Exception ex)
            {
                // Log exception
                _logger?.LogError(ex, "Error while fetching access details");
                throw;
            }
        }

        public async Task<BasicMenuDTO> GetBasicMenuDTO(int? employeeTypeId, int forPlatform)
        {
            try
            {
                _logger?.LogInformation("Fetching basic menu for EmployeeTypeId: {EmployeeTypeId}, Platform: {ForPlatform}", employeeTypeId, forPlatform);

                var menus = await _context.ETBasicMenuAccess
                              .Where(m => m.EmployeeTypeId == 6)
                              .ToListAsync();
                //return menus;
              var tttt=  menus;
                // Return the result as BasicMenuDTO
                var basicMenu = new BasicMenuDTO
                {
                    Menus = null,
                   // TotalCount = menus.Count
                };

                _logger?.LogInformation("Basic menu fetched successfully for EmployeeTypeId: {EmployeeTypeId}, Platform: {ForPlatform}", employeeTypeId, forPlatform);

                return basicMenu;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while fetching the basic menu for EmployeeTypeId: {EmployeeTypeId}, Platform: {ForPlatform}", employeeTypeId, forPlatform);
                throw new Exception("Failed to fetch basic menu. Please try again later.", ex);
            }
            //Max Pool Size=100;
        }

    }

}
