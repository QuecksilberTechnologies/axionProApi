using ems.domain.Entity.CommonMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface ICommonMenuRepository
    {
        Task<IEnumerable<CommonMenu>> GetMenusByUserAndDeviceAsync(long userId, int deviceType);
        
    }
}
