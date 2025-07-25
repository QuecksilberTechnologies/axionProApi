﻿using ems.domain.Common;
using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Asset : BaseEntity
{
    public int Id { get; set; }

    public long? TenantId { get; set; } 

    public string AssetName { get; set; }

    public int AssetTypeId { get; set; }

    public string Company { get; set; } 

    public string? Color { get; set; }

    public bool? IsRepairable { get; set; }

    public decimal? Price { get; set; }

    public string? SerialNumber { get; set; }

    public string? Barcode { get; set; }

    public string? Qrcode { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime? WarrantyExpiryDate { get; set; }

    public int AssetStatusId { get; set; }

    public bool? IsAssigned { get; set; }  

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual AssetStatus AssetStatus { get; set; } = null!;

    public virtual AssetType AssetType { get; set; } = null!;
}

 