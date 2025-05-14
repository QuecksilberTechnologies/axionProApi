using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.ProjectModule
{
    public class ProjectModuleDetailDTO
    {
        public int Id { get; set; }
        public string? ModuleName { get; set; }
        public string? ModuleURL { get; set; }
        public bool IsActive { get; set; }
        public string? Remark { get; set; }
        public string? IconImage { get; set; }
        public List<ProjectSubModuleDetailDTO> SubModules { get; set; } = new List<ProjectSubModuleDetailDTO>();

    }

}

 
