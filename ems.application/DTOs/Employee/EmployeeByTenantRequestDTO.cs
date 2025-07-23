using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Employee
{
    /// <summary>
    /// post-request to fetch all employee 
    /// </summary>
     
    public class EmployeeByTenantRequestDTO
    {
        /// <summary> TenantId Required</summary>
        // ✅ Required Fields
        [Required]
        public long TenantId {  get; set; }
    }
}
