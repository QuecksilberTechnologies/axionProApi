﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    public class GetAllEmployeeTypeDTO
    {
        public int EmployeeTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public bool IsActive { get; set; }
        public long AddedById { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }



    }
}
