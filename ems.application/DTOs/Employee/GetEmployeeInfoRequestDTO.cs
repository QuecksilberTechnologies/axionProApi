using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
   public class GetEmployeeInfoRequestDTO
    {
        public long TenantId { get; set; }
        public long EmployeeId { get; set; }
    }
}
