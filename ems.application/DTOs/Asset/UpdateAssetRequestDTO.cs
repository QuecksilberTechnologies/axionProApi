﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
   

        public class UpdateAssetRequestDTO
        {
            public int Id { get; set; }
            public int RoleId { get; set; }
            public long? TenantId { get; set; }
            public long UpdatedById { get; set; }
            public string? AssetName { get; set; }   // ✅ Nullable string
            public int? AssetTypeId { get; set; }
            public string? Company { get; set; }     // ✅ Nullable string
            public string? Color { get; set; }
            public bool? IsRepairable { get; set; }
            public decimal? Price { get; set; }
            public string? SerialNumber { get; set; }
            public string? Barcode { get; set; }
            public string? Qrcode { get; set; }
            public DateTime? PurchaseDate { get; set; }
            public DateTime? WarrantyExpiryDate { get; set; }
            public int? AssetStatusId { get; set; }
            public bool? IsAssigned { get; set; }
            public bool? IsActive { get; set; }

        }




    }



