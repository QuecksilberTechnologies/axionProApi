using ems.domain.Common;
using System;

namespace ems.domain.Entity.UserCredential
{
    public class LoginCredential: BaseEntity
    {
        public long Id { get; set; }                        // bigint -> long
        public string EmployeeId { get; set; }              // nvarchar(255) -> string
        public string LoginId { get; set; }                 // nvarchar(255) -> string
        public string Password { get; set; }                // nvarchar(255) -> string
        public bool HasFirstLogin { get; set; }             // bit -> bool
        public string MacAddress { get; set; }              // nvarchar(255) -> string
        public string IpAddress { get; set; }               // nvarchar(255) -> string
        public bool IsActive { get; set; }                  // bit -> bool
        public string Remark { get; set; }                  // nvarchar(255) -> string
        public double Latitude { get; set; }                 // float -> float
        public double Longitude { get; set; }                // float -> float
        public int LoginDevice { get; set; }                // int -> int
           // datetime -> DateTime (nullable)
    }
}
