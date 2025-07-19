using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class UpdateGenricAllEmployeeEntityRequestDTO
    {
        public long EmployeeId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public object? FieldValue { get; set; }
        public long UpdatedById { get; set; }
    }
}
