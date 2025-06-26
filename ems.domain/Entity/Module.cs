using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ems.domain.Entity;

public partial class Module
{
    public int Id { get; set; }

    public string? ModuleCode { get; set; }

    public string ModuleName { get; set; } = null!;

    public string? SubModuleUrl { get; set; }

    public int? ParentModuleId { get; set; }

    public bool IsModuleDisplayInUi { get; set; }

    public bool? IsCommonMenu { get; set; }

    public bool IsActive { get; set; }

    public string? ImageIconWeb { get; set; }

    public string? ImageIconMobile { get; set; }

    public string? Remark { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<Module> InverseParentModule { get; set; } = new List<Module>();

    public virtual Module? ParentModule { get; set; }

    public virtual ICollection<PlanModuleMapping> PlanModuleMappings { get; set; } = new List<PlanModuleMapping>();
    public virtual ICollection<ModuleOperationMapping> ModuleOperationMappings { get; set; } = new List<ModuleOperationMapping>();


    public virtual ICollection<TenantEnabledModule> TenantEnabledModules { get; set; } = new List<TenantEnabledModule>();

    public virtual ICollection<TenantEnabledOperation> TenantEnabledOperations { get; set; } = new List<TenantEnabledOperation>();

    [NotMapped] // ✅ This solves the "Invalid column name 'ModuleId'" error
    public virtual List<Module> ChildModules { get; set; } = new();

}



public class ModuleDTO
{
    public int Id { get; set; }
    public string ModuleName { get; set; }
    public string? SubModuleUrl { get; set; }
    public List<ModuleDTO> Children { get; set; } = new();
}

