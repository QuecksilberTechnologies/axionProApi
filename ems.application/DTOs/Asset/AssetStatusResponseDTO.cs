using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class AllAssetStatusResponseDTO
    {
        public long Id { get; set; }
        public long TenantId { get; set; }       
        public string StatusName { get; set; } = string.Empty;
        public string? Description { get; set; }
 
    }
}
