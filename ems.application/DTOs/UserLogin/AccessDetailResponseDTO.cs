using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.DTOs.UserLogin
{
    public class AccessDetailResponseDTO
    {
        public long EmployeeId { get; set; }
        public int? ForPlatform { get; set; }
        public IEnumerable<BasicMenuDTO> Menus { get; set; } = new List<BasicMenuDTO>();
    }

}
