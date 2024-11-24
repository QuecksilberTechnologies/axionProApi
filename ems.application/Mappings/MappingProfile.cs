using AutoMapper;
using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.EmployeeDTO;
using ems.application.DTOs.RoleDTO;
using ems.application.DTOs.UserLogin;
using ems.domain.Entity;
 
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
            CreateMap<LoginEmployeeInfoDTO, LoginResponseDTO>()
             .ForMember(dest => dest.EmployeeInfo, opt => opt.MapFrom(src => src));


            // Mapping Employee entity to LoginEmployeeInfoDTO
            CreateMap<Employee, LoginEmployeeInfoDTO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id)) // Map EmployeeId
                .ForMember(dest => dest.LoginId, opt => opt.MapFrom((src, dest, _, context) =>
                    context.Items["LoginId"] as string)) // Map LoginId from context

                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName)) // Map UserName
                .ForMember(dest => dest.EmployeeMiddleName, opt => opt.MapFrom(src => src.MiddleName)) // Map UserName
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName)) // Map UserName
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => ((src.FirstName)+ src.MiddleName)+ src.LastName)) // Map UserName
                .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.EmployeeTypeId.ToString())) // Map EmployeeTypeId
               // .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployementType.ToString())) // Map EmployeeTypeId
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployementType.TypeName)) // Map EmployeeType Name
                .ForMember(dest => dest.EmployeeAssignedRoles, opt => opt.MapFrom(src => src.UserRolesEmp.Select(ur => new RoleInfoDTO
                {
                    Id = ur.RolesUr.Id,
                    RoleName = ur.RolesUr.RoleName,
                    Description = ur.Remark
                    
                })));






            // CreateMap<LoginRequestDTO, LoginCredential>();

            // Map Role to RoleInfoDTO
            // CreateMap<Role, LoginResponseDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //   .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
            //   .ForMember(dest => dest. opt => opt.MapFrom(src => src.IsActive));


            // `Role` to `GetRoleByIdDTO` mapping
            // Agar reverse mapping bhi chahiye toh isko bhi add kar sakte hain

        }

    }
}
