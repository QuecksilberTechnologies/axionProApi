using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Operation
{
   
    public class HasAccessOperationDTO
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public bool? Status { get; set; }
    }
}
