using System;
using System.Collections.Generic;

namespace ems.domain.Entity;

public partial class Category
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Remark { get; set; }

    public string? Code { get; set; }

    public int Depth { get; set; }

    public string? Tags { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }
}
