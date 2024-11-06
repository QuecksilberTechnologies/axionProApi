using ems.application.Interfaces.IRepositories;
using ems.domain.Entity.CommonMenu;
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
    public class CommonMenuRepository : ICommonMenuRepository
    {
        private readonly EmsDbContext _context;
        private readonly ILogger<CommonMenuRepository> _logger;

        

        public CommonMenuRepository(EmsDbContext context)
        {
            _context = context;
            //_logger = logger;
        }

        public EmsDbContext Context { get; }

        public async Task<IEnumerable<CommonMenu>> GetMenusByUserAndDeviceAsync(long userId, int deviceType)
        {
         //   _logger.LogInformation("Fetching menu list for User ID: {UserId} on Device Type: {DeviceType}", userId, deviceType);

            try
            {


                var menus = await _context.CommonMenus
                    .Where(menu => menu.IsActive && menu.HasAccess &&
                        (menu.ForPlatform == deviceType || menu.ForPlatform == null))
                    .ToListAsync();

               // _logger.LogInformation("Fetched {MenuCount} menus for User ID: {UserId}", menus.Count, userId);
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
