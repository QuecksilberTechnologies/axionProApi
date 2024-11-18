using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IAccessDetailRepository
    {
      public  Task<AccessDetailResponseDTO> GetAccessDetailResponseDTO(AccessDetailRequestDTO? accessDetailRequest);
      public  Task<BasicMenuDTO> GetBasicMenuDTO(int? employeeTypeId, int forPlatform);
    }
}
