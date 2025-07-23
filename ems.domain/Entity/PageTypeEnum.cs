using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.domain.Entity
{
    public class PageTypeEnum
    {
        public int Id { get; set; }

        public string? PageTypeName { get; set; }

        public virtual ICollection<ModuleOperationMapping> ModuleOperationMappings { get; set; } = new List<ModuleOperationMapping>();

    }

}
