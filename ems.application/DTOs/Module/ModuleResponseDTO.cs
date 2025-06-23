using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module
{
    public class ModuleResponseDTO
    {

        public int ModuleId { get; set; }
        public int? ParentModuleId { get; set; }
        public string? ModuleName { get; set; }


    }
}
