using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.EmployeeDTO
{
    
        public class CreateEmployeeDTO
    {
           
        
        public long EmployeeDocumentId { get; set; }
        public string EmployementCode { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string FirstName { get; set; } = null!;
        // Add more fields as per your requirement
    }
    
    

}
 
