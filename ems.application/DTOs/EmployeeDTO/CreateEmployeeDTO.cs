using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.EmployeeDTO
{
    
        public class CreateEmployeeDTO
    {
            public string EmployementCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string OfficialEmail { get; set; }
            public DateTime DateOfBirth { get; set; }
            public DateTime DateOfOnBoarding { get; set; }
            public int DepartmentId { get; set; }
            // Add more fields as per your requirement
        }
    
    

}
 
