using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Designation
{
    public class UpdateDesignationDTO
    {

        public int Id { get; set; }
        public long TenantId { get; set; }
        public string DesignationName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
