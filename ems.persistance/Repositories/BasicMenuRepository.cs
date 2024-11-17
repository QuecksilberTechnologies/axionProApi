using ems.application.Constants;

using ems.application.Interfaces.IRepositories;
using ems.domain.Entity.BasicMenuInfo;
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
    public class BasicMenuRepository : IBasicMenuRepository
    {
        private readonly EmsDbContext _context;
        private readonly ILogger _logger;



        public BasicMenuRepository(EmsDbContext context, ILogger<BasicMenuRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<IEnumerable<BasicMenu>> GetBasicMenusByUserAndDeviceAsync(long userId, int deviceType)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BasicMenu>> GetCommonMenusByUserAndDeviceAsync(long userId, int deviceType)
        {
         //   _logger.LogInformation("Fetching menu list for User ID: {UserId} on Device Ty0pe: {DeviceType}", userId, deviceType);

            try
            {
                var query = _context.BasicMenus.AsQueryable();
                // Check login device and filter accordingly
                if (deviceType == AppConstants.DeviceTypeWeb)
                {
                 //   query = query.Where(m => m.ForPlatform== (AppConstants.DeviceTypeWeb| AppConstants.DeviceTypeForAll) && (m.HasAccess && m.IsActive)); // Assuming `IsForWeb` denotes web-specific menus
                }
                else if (deviceType == AppConstants.DeviceTypeMobile)
                {
                  //  query = query.Where(m => m.ForPlatform == (AppConstants.DeviceTypeMobile | AppConstants.DeviceTypeForAll) && (m.HasAccess && m.IsActive)); // Assuming `IsForMobile` denotes mobile-specific menus
                }
                else
                {
                    // If no specific platform (deviceType = 0), fetch all active menus with access
                  //  query = query.Where(m => m.HasAccess && m.IsActive && (m.ForPlatform == AppConstants.DeviceTypeWeb || m.ForPlatform == AppConstants.DeviceTypeMobile));
                }

                var menus = await query.ToListAsync();

                 _logger.LogInformation("Fetched... {MenuCount} menus for User ID: {UserId}", menus.Count, userId);
                return menus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching menus for User ID: {UserId} and Device Type: {DeviceType}", userId, deviceType);
                throw;
            }
        }

       
    }

}
