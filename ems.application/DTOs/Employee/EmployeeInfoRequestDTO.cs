using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{

    /// <summary>
    /// post-request to fetch employee-info  
    /// </summary>
    public class EmployeeInfoRequestDTO
    {
        /// <summary> TenantId Required</summary>

        [Required]
        public long TenantId { get; set; }

        /// <summary> EmployeeId Required</summary>

        [Required]
        public long EmployeeId { get; set; }
    }
}
