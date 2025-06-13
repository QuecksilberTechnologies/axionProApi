using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Asset
{
    public class AddAssetStatusRequestDTO
    {
        
        public long TenantId { get; set; }
        public long EmployeeId { get; set; }  
        public int  RoleId { get; set; }       
        public string? StatusName { get; set; }  
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public long AddedById { get; set; }


    }
}
