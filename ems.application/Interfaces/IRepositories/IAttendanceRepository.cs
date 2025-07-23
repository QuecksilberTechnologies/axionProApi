using ems.application.DTOs.AttendanceDTO;
using ems.domain.Entity;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IAttendanceRepository
    {
      public Task<UserAttendanceSetting> GetUserAttendanceSettingByIdAsync(AttendanceRequestDTO attendanceRequestDTO);
      public Task<bool> AddEmployeeAttendanceAsync(AttendanceRequestDTO attendanceRequestDTO);

    }
}
