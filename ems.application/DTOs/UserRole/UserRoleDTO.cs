using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserRole 
{
    public class UserRoleDTO
    {  
        public int Id { get; set; }
        public long EmployeeId { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
      
        public bool IsPrimaryRole { get; set; }
        public string Remark { get; set; }
        public DateTime AssignedDateTime { get; set; }
        public DateTime? RemovedDateTime { get; set; }
        public long AssignedById { get; set; }
        public DateTime RoleStartDate { get; set; }
        public long AddedById { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
       
    } 

    
}
