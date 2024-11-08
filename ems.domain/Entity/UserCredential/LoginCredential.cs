using ems.domain.Common;
using System;

namespace ems.domain.Entity.UserCredential
{
    public class LoginCredential: BaseEntity
    {
        public long Id { get; set; }                        // bigint -> long
    
        public long EmployeeId { get; set; }

        public string? LoginId { get; set; }

        public string? Password { get; set; }

        public bool? HasFirstLogin { get; set; }

        public string? MacAddress { get; set; }

        public string? IpAddress { get; set; }

        public bool? IsActive { get; set; }

        public string? Remark { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int LoginDevice { get; set; }

        // datetime -> DateTime (nullable)
    }
}
