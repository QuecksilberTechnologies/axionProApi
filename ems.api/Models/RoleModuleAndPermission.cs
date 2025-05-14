using System;
using System.Collections.Generic;

namespace ems.api.Models;

public partial class RoleModuleAndPermission
{
    public int Id { get; set; }

    public int? SubModuleId { get; set; }

    public int? RoleId { get; set; }

    public int? OperationId { get; set; }

    public bool? HasAccess { get; set; }

    public bool? IsActive { get; set; }

    public string? Remark { get; set; }

    public string? ImageIcon { get; set; }

    public int? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public int? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ProjectSubModuleDetail? SubModule { get; set; }
}
