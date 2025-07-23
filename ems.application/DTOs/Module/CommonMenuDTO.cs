using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Module
{
    public class CommonItemDTO
    {
        public int Id { get; set; }      
        public string? ModuleName { get; set; }
        public string? SubModuleUrl { get; set; }
        public List<CommonItemDTO>? Children { get; set; } = new();
    }

    
}
