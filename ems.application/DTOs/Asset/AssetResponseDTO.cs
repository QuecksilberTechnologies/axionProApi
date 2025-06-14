using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class AssetResponseDTO
    {
        public int Id { get; set; }
        public long TenantId { get; set; }  
        public string AssetName { get; set; }
        public int AssetTypeId { get; set; }
        public string Company { get; set; }
        public string Color { get; set; }
        public bool IsRepairable { get; set; }
        public decimal Price { get; set; }
        public string SerialNumber { get; set; }
        public string Barcode { get; set; }
        public string QRCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyExpiryDate { get; set; }
        public int AssetStatusId { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsActive { get; set; }
    }
}
