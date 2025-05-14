﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Client
{
    public class CreateClientTypeDTO
    {
        public string TypeName { get; set; } //  
        public bool IsActive { get; set; } = true;
        public string? Remark { get; set; } // 
        public string? Description { get; set; } // 
    }
}
