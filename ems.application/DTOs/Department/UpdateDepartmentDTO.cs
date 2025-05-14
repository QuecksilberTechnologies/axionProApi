﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Department
{
    public class UpdateDepartmentDTO
    {
        public int Id { get; set; } // यूज़र ID जिसने ऐड किया

        public string DepartmentName { get; set; }  // अनिवार्य फ़ील्ड
        public string? Description { get; set; } // वैकल्पिक (nullable) फ़ील्ड

        public bool IsActive { get; set; } = true; // डिफ़ॉल्ट रूप से Active
        public string? Remark { get; set; } // वैकल्पिक (nullable) फ़ील्ड


        public int? UpdatedById { get; set; } // Nullable, क्योंकि अपडेट हो सकता है या नहीं
        public DateTime? UpdatedDateTime { get; set; } // Nullable, क्योंकि अपडेट हो सकता है या नहीं

    }
}
