using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Department
{
    /// <summary>
    /// post-request to fetch all department 
    /// </summary>

    public class DepartmentRequestDTO
    {

        /// <summary> TenantId Required</summary>
       
        [Required]
        public long? TenantId { get; set; }
        public long? RoleId { get; set; }


        
    }
}
