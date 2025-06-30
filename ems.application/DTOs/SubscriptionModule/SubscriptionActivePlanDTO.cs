using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.SubscriptionModule
{
    public class SubscriptionActivePlanDTO
    {
        public int Id { get; set; }
        public string? PlanName { get; set; }
        public List<ModuleActiveDTO>? Modules { get; set; }
    }

    public class ModuleActiveDTO
    {
       public int?  ParentModuleId { get; set; } 
       public string  ParentModuleName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ?ModuleName { get; set; }
        public List<OperationActiveDTO>? Operations { get; set; }
    }

    public class OperationActiveDTO
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? PageUrl { get; set; }
    }

}
