using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.ModuleOperation
{
    public class ModuleOperationMappingResponseDTO
    {
        public int ModuleId { get; set; }
        public List<int> OperationIds { get; set; } = new();
        public string? Message { get; set; }
        public bool Success { get; set; }
    }

}
