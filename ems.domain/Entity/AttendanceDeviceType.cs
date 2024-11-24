using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity
{
    public partial class AttendanceDeviceType
    {
        public int Id { get; set; }

        public string? DeviceType { get; set; }

        public string? Remark { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeviceRegister { get; set; }

        public long AddedById { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public long? UpdatedById { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public virtual ICollection<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; } = new List<EmployeeDailyAttendance>();

        public virtual ICollection<UserAttendanceSetting> UserAttendanceSettings { get; set; } = new List<UserAttendanceSetting>();
    }

}
