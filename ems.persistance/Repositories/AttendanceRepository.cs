using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
       
        public Task<IEnumerable<UserAttendanceSetting>> GetUserAttendanceSettingByIdAsync(long userId, int attendanceDeviceId, int workstationTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
