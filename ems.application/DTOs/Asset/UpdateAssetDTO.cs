using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class UpdateAssetDTO
    {

        public int Id { get; set; }
        public bool IsRepairable { get; set; }
        public int AssetTypeId { get; set; }
        public DateTime WarrantyExpiryDate { get; set; }
        public int AssetStatusId { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsActive { get; set; }
    }
}
