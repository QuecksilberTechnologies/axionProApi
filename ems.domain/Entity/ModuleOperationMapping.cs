using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class ModuleOperationMapping
{
    public int Id { get; set; }
    public int? ModuleId { get; set; }

    public int OperationId { get; set; }
    public int DataViewStructureId { get; set; }

  //  public string? DisplayName { get; set; }
    public int PageTypeId { get; set; }  
    public string? PageUrl { get; set; }

    public string? IconUrl { get; set; }

    public bool? IsCommonItem { get; set; }

    public bool? IsOperational { get; set; }

    public int? Priority { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
 
    public virtual Operation Operation { get; set; } = null!;
    public virtual Module? Module { get; set; }
 
    public virtual DataViewStructure? DataViewStructure { get; set; }
    public virtual PageTypeEnum? PageType { get; set; }
    



}
