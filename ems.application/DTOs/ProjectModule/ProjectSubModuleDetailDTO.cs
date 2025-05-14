﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.ProjectModule
{
    public class ProjectSubModuleDetailDTO
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }  // Foreign key to ProjectModuleDetail
        public string? SubModuleName { get; set; }
        public string? SubModuleURL { get; set; }
        public bool? IsSubModuleDisplayInUI { get; set; }
        public bool? IsActive { get; set; }
        public string? Remark { get; set; }
        public List<ProjectChildModuleDetailDTO>? ChildPage { get; set; }


    }

}
