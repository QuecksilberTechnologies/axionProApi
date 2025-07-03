using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Operation
{
    public class CreateOperationByProductOwnerRequestDTO
    {

        public int ProductOwnerId { get; set; }
        public int ProductOwnerRoleId { get; set; }
        public string OperationName { get; set; }  // Default value
        public string? Remark { get; set; } // Nullable
        public bool? IsActive { get; set; } = false; // Default false
        public long? AddedById { get; set; } // Nullable
        public DateTime? AddedDateTime { get; set; }  // Default value
    }
}
