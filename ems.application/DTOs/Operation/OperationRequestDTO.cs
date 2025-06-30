using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Operation
{
    public class GetAllOperationRequestByProductAdminDTO
    {
        public int ProductOwnerId { get; set; }
        public int ProductOwnerRoleId { get; set; } 
        
    }
}
