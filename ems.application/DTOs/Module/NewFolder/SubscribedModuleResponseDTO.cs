﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module.NewFolder
{

    [Keyless]
    public class SubscribedModuleResponseDTO
    {
            public int ModuleId { get; set; }
            public string ModuleName { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
        }
    

}
