using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Module
{
    public int Id { get; set; }

    public string? ModuleCode { get; set; }

    public string ModuleName { get; set; } = null!;

    public string? SubModuleUrl { get; set; }

    public int? ParentModuleId { get; set; }

    public bool IsModuleDisplayInUi { get; set; }

    public bool IsActive { get; set; }
    public bool IsCommonMenu { get; set; }

    public string? ImageIconWeb { get; set; }

    public string? ImageIconMobile { get; set; }

    public string? Remark { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
    public virtual Module? ParentModule { get; set; } // 👈 Parent

 
    public virtual ICollection<Module> InverseParentModule { get; set; } = new List<Module>();

    public virtual ICollection<ModuleOperationMapping> ModuleOperationMappings { get; set; } = new List<ModuleOperationMapping>();
    public virtual List<Module> ChildModules { get; set; } = new(); // ✅ Correct collection

    public virtual ICollection<PlanModuleMapping> PlanModuleMappings { get; set; } = new List<PlanModuleMapping>();

}
