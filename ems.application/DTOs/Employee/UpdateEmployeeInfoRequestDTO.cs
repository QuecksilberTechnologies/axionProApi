using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class UpdateEmployeeInfoRequestDTO
    {
        public long EmployeeId { get; set; }

        public Dictionary<string, object?> FieldsToUpdate { get; set; } = new();
    }

}
