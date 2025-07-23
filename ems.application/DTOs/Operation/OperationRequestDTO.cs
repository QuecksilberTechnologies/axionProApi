using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Operation
{

    /// <summary>
    /// get-request to get all operation
    /// </summary>
    public class GetOperationRequestDTO
    {
        /// <summary> Employee Id Required</summary>
       
        public required long EmployeeId { get; set; }
        /// <summary> Role Id Required</summary>
        public required int RoleId { get; set; } 
        
    }
}
