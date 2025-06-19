using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.Registration
{
    public class TenantRequestDTO
    {
       
        public string CompanyName { get; set; } = null!;

        public string TenantCode { get; set; } = null!;
 
        public string CompanyEmailDomain { get; set; } = null!;
         
        public string TenantEmail { get; set; } = null!;

        public string? ContactPersonName { get; set; }

        public string? ContactNumber { get; set; }
      
        public int CountryId { get; set; }     
 
 
    }

}
