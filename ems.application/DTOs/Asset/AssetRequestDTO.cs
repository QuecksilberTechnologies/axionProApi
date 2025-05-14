using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class AssetRequestDTO
    {
        public long Id { get; set; }
        public int SelfRoleId { get; set; }
        public int DesignationId { get; set; }
        public int AssetId { get; set; }

    }

}
