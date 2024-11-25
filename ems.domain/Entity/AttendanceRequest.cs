using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class AttendanceRequest
{
    public int Id { get; set; }

    public long EmployeeId { get; set; }

    public DateTime AttendanceDate { get; set; }

    public int AttendanceDeviceTypeId { get; set; }

    public int WorkstationTypeId { get; set; }

    public double? Latitude { get; set; }

    public bool? IsActive { get; set; }

    public string? Remark { get; set; }

    public double? Longitude { get; set; }

    public byte[]? ClickedImage { get; set; }

    public long AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}
