using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.RoleDTO
{
    public class CreateRoleDTO
    {
       
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public string IsActive { get; set; }
        public long  AddedById { get; set; }
        public DateTime AddedDateTime { get; set; }
        
         

    }
}
