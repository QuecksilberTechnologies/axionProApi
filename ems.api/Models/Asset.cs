using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class Asset
{
    public int Id { get; set; }

    public string AssetName { get; set; } = null!;

    public int AssetTypeId { get; set; }

    public string Company { get; set; } = null!;

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

    public bool? IsActive { get; set; }

    public long AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual AssetStatus AssetStatus { get; set; } = null!;

    public virtual AssetType AssetType { get; set; } = null!;
}
