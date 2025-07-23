using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{

    /// <summary>
    /// post-request to fetch all asset status
    /// </summary>
    public class AssetStatusRequestDTO
    {
        /// <summary> TenantId Required</summary>
        [Required]


        public long? TenantId { get; set; }
        /// <summary> Employee Id Required</summary>

        [Required]
        public long EmployeeId { get; set; }
        /// <summary> TenantId Required</summary>
        public int  RoleId { get; set; }

        /// <summary> If not Required let it null</summary>
        public string? StatusName { get; set; }
        /// <summary> If Description is not Required let it null</summary>
        public string? Description { get; set; }
        /// <summary> IsActive Required true/false</summary>
        [Required]
        public bool IsActive { get; set; }
     


    }
}
