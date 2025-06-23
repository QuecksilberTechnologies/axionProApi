using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module
{
    public class CreateModuleRequestDTO
    {
        public long? ParentModuleId { get; set; } = null;

        public string? ModuleCode { get; set; }

        public string ModuleName { get; set; } = null!;

        public string? SubModuleURL { get; set; }

        public bool IsModuleDisplayInUI { get; set; }

        public bool IsActive { get; set; }

        public string? ImageIconWeb { get; set; }

        public string? ImageIconMobile { get; set; }

        public string? Remark { get; set; }

   
    }

}
