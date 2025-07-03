using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class AssetCreateRequestByTenantUserDTO
    {
        public long? TenantId { get; set; }
        public int RoleId { get; set; }
        public long EmployeeId { get; set; }
        public bool? IsActive { get; set; }
        public int? AssetTypeId { get; set; }
        public int? AssetStatusId { get; set; }
        public bool? IsAssigned { get; set; }
        public string? Company { get; set; }
        public string? AssetName { get; set; }
        public string? Color { get; set; }
        public bool? IsRepairable { get; set; }
        public string? SerialNumber { get; set; }
        public string? Barcode { get; set; }
        public string? Qrcode { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public long? AddedById { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }


}
