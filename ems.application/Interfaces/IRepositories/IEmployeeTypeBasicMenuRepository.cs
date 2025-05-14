using ems.application.DTOs.BasicAndRoleBaseMenu;
using ems.application.DTOs.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IEmployeeTypeBasicMenuRepository
    {
        //   public  Task<AccessDetailResponseDTO> GetAccessDetailResponseDTO(AccessDetailRequestDTO? accessDetailRequest);
        public  Task<IEnumerable<BasicMenuDTO>> GetBasicMenuDTO(int? employeeTypeId, int? forPlatform);
      

    }
}
