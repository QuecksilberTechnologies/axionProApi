using AutoMapper;
using ems.application.DTOs.EmployeeDTO;
using ems.domain.Entity.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ems.application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee Mappings
            CreateMap<Employee, CreateEmployeeDTO>();
           

            // Department Mappings
         //   CreateMap<Department, DepartmentDto>();
         //CreateMap<DepartmentCreateDto, Department>();
            // Add more mappings as needed
        }
    }
}
