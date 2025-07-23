using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Designation
{
    public class CreateDesignationDTO
    {

        public long? TenantId { get; set; }
        public int? DepartmentId { get; set; }
        public string DesignationName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public long AddedById { get; set; }
        public DateTime AddedDateTime { get; set; }


    }

}
