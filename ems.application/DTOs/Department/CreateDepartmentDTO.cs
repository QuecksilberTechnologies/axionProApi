using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Department
{
    public class CreateDepartmentDTO
    {
        public string DepartmentName { get; set; }  // अनिवार्य फ़ील्ड
        public string? Description { get; set; } // वैकल्पिक (nullable) फ़ील्ड

        public bool IsActive { get; set; } = true; // डिफ़ॉल्ट रूप से Active
        public string? Remark { get; set; } // वैकल्पिक (nullable) फ़ील्ड

        public int AddedById { get; set; } // यूज़र ID जिसने ऐड किया
        public DateTime AddedDateTime { get; set; } = DateTime.UtcNow; // ऑटो सेट

    }

}
