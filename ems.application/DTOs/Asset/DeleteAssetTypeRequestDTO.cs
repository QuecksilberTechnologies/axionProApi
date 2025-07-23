using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class DeleteAssetTypeRequestDTO
    {
        public int Id { get; set; }
        public long? TenantId { get; set; }
        public long DeletedById { get; set; }
    }
}
