using AutoMapper;
using ems.application.DTOs.EmployeeDTO;
using ems.application.DTOs.UserLogin;
using ems.domain.Entity.EmployeeModule;
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

            CreateMap<LoginCredential, LoginRequestDTO>();
            // Agar reverse mapping bhi chahiye toh isko bhi add kar sakte hain
            CreateMap<LoginRequestDTO, LoginCredential>();

        }

    }
}
