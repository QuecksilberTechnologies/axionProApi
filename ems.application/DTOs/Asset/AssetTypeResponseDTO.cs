using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class AssetTypeResponseDTO
    {
        public int Id { get; set; }
        public long TenantId { get; set; }
        public string TypeName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
       
    
    }
}
