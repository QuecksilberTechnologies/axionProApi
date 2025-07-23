using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Leave
{
    public class CreateLeaveTypeDTO
    {
        public string LeaveName { get; set; }  // Default value
        public string? Description { get; set; } // Nullable
        public bool IsActive { get; set; } = false; // Default false
        public long? AddedById { get; set; } // Nullable
        public DateTime AddedDateTime { get; set; } = DateTime.UtcNow; // Default value


    }
}
