using ems.domain.Entity.BasicMenuInfo;
using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.Masters.RoleInfo;
using ems.domain.Entity.UserCredential;
using ems.domain.Entity.UserRoleModule;

public partial class EmployeeType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public int? RoleId { get; set; }

    public string? Description { get; set; }

    public string? Remark { get; set; }

    public bool? IsActive { get; set; }

    public long? AddedById { get; set; }

    public DateTime? AddedDateTime { get; set; }

    public long? UpdatedById { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<BasicMenu> BasicMenu { get; set; } = new List<BasicMenu>();
    public ICollection<Employee>? Employees { get; set; } = null;

    // public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistoryNewEmployeeTypes { get; set; } = new List<EmployeeStatusHistory>();

    // public virtual ICollection<EmployeeStatusHistory> EmployeeStatusHistoryOldEmployeeTypes { get; set; } = new List<EmployeeStatusHistory>();


    public virtual Role? RoleInfo { get; set; }
}
