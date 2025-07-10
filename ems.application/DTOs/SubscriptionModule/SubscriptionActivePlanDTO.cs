using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.SubscriptionModule
{
    public class SubscriptionActivePlanDTO
    {
        public long Id { get; set; }
        public string? PlanName { get; set; }
        public List<ModuleActiveDTO>? Modules { get; set; }
    }

    public class ModuleActiveDTO
    {
        public long Id { get; set; }
        public string? ModuleName { get; set; }
        public string? DisplayName { get; set; }
        public long ParentModuleId { get; set; }
        public List<ModuleActiveDTO> ChildModules { get; set; } = new();
        public List<OperationActiveDTO> Operations { get; set; } = new();
    }

    public class OperationActiveDTO
    {
        public long Id { get; set; }
        public string? DisplayName { get; set; }
    }


}
