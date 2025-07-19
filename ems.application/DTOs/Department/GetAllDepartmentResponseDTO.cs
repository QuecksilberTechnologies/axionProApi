using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Department
{
    public class GetAllDepartmentResponseDTO
    {
        public int Id { get; set; }
        public long TenantId { get; set; }
        public long TenantIndustryId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;       
        public bool IsActive { get; set; }

     
    }
}
