using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Attendance
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public int DeviceId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public byte[]? ClickedImage { get; set; }

    public long AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}
