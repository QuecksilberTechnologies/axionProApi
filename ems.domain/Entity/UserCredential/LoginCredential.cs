using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity.UserCredential
{
    public class LoginCredential
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public bool HasFirstLogin { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
        public long AddedById { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }

}
