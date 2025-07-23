using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Leave
{
    public class UpdateLeaveTypeDTO
    {
        public int Id { get; set; }
        public string LeaveName { get; set; } = string.Empty;
        public string? Description { get; set; } // Nullable
        public bool IsActive { get; set; } = false; // Default false


        //public long? UpdatedById { get; set; } // Nullable
        //public DateTime UpdatedDateTime { get; set; } = DateTime.UtcNow; // Default value

    }
}
