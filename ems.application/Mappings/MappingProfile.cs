using AutoMapper;
using ems.application.DTOs.CommonAndRoleBaseMenu;
using ems.application.DTOs.EmployeeDTO;
using ems.application.DTOs.RoleDTO;
using ems.application.DTOs.UserLogin;
using ems.domain.Entity.CommonMenu;
using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.Masters.RoleInfo;
using ems.domain.Entity.UserCredential;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
 

namespace ems.application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

             
                // Map Employee to EmployeeDTO
                CreateMap<Employee, EmployeeDTO>();
                // Agar reverse mapping chahiye toh, isse bhi add kar sakte hain
                CreateMap<EmployeeDTO, Employee>();
            CreateMap<LoginRequestDTO, LoginCredential>();

            CreateMap<CommonMenu, CommonMenuDTO>();
            CreateMap<Role, GetRoleByIdDTO>(); // `Role` to `GetRoleByIdDTO` mapping
            // Agar reverse mapping bhi chahiye toh isko bhi add kar sakte hain

        }

    }
}
