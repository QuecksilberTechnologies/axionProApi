using AutoMapper;
using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.CategoryDTO;
using ems.application.DTOs.EmployeeDTO;
using ems.application.DTOs.RegistrationDTO;
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
        private static string CleanSkillSet(string skillSet)
        {
            if (string.IsNullOrEmpty(skillSet))
                return string.Empty;

            return string.Join(",", skillSet.Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
            );
        }

        public MappingProfile()
        {

             
                // Map Employee to EmployeeDTO
                CreateMap<Employee, EmployeeDTO>();
             
                CreateMap<CreateRoleDTO, Role>();  // ✅ Yeh likhna hoga!
                                                // Role Entity to GetAllRoleDTO Mapping
                // Direct entity to DTO mapping
               CreateMap<Role, GetAllRoleDTO>();

             // CreateRoleDTO to Role Mapping (for creating roles)
          
        
 
       
        // Agar reverse mapping chahiye toh, isse bhi add kar sakte hain
               CreateMap<EmployeeDTO, Employee>();
                CreateMap<LoginEmployeeInfoDTO, LoginResponseDTO>().ForMember(dest => dest.EmployeeInfo, opt => opt.MapFrom(src => src));
                CreateMap<Category, CategoryResponseDTO>();


            // Add mapping for List<Role> to List<GetAllRoleDTO>
            // CreateMap<List<Role>, List<GetAllRoleDTO>>();  // Add this line

             // CreateMap<List<Role>, List<GetAllRoleDTO>>()
              //.ForMember(dest => dest, opt => opt.MapFrom(src => src.Select(x => new GetAllRoleDTO { /* Manually map properties here */ }).ToList()));
             

            // Mapping Employee entity to LoginEmployeeInfoDTO
            CreateMap<Employee, LoginEmployeeInfoDTO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id)) // Map EmployeeId
                .ForMember(dest => dest.LoginId, opt => opt.MapFrom((src, dest, _, context) =>context.Items["LoginId"] as string)) // Map LoginId from context
                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName)) // Map UserName
                .ForMember(dest => dest.EmployeeMiddleName, opt => opt.MapFrom(src => src.MiddleName)) // Map UserName
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName)) // Map UserName
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => ((src.FirstName)+ src.MiddleName)+ src.LastName)) // Map UserName
                .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.EmployeeTypeId.ToString())) // Map EmployeeTypeId
               // .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployementType.ToString())) // Map EmployeeTypeId
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType.TypeName)) // Map EmployeeType Name
               .ForMember(dest => dest.EmployeeAssignedRoles, opt => opt.MapFrom(src => src.UserRolesEmp.Select(ur => new RoleInfoDTO
               {
                   Id = ur.RoleId.GetValueOrDefault(),  // ✅ Fix: Converts nullable int? to int (Defaults to 0 if null)
                   RoleName = ur.Role.RoleName,
                   Description = ur.Remark
               })));



            //    CreateMap<CandidateRequestDTO, Candidate>()
            // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            // .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            // .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            // .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            // .ForMember(dest => dest.Pan, opt => opt.MapFrom(src => src.Pan))
            // .ForMember(dest => dest.Aadhaar, opt => opt.MapFrom(src => src.Aadhaar))
            // .ForMember(dest => dest.CandidateReferenceCode, opt => opt.MapFrom(src => src.CandidateReferenceCode))
            // .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.ResumeUrl))
            // .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.ExperienceYears))
            // .ForMember(dest => dest.CurrentLocation, opt => opt.MapFrom(src => src.CurrentLocation))
            // .ForMember(dest => dest.ExpectedSalary, opt => opt.MapFrom(src => src.ExpectedSalary))
            // .ForMember(dest => dest.CurrentCompany, opt => opt.MapFrom(src => src.CurrentCompany))
            // .ForMember(dest => dest.NoticePeriod, opt => opt.MapFrom(src => src.NoticePeriod))
            // .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            // .ForMember(dest => dest.AppliedDate, opt => opt.MapFrom(src => src.AppliedDate))
            // .ForMember(dest => dest.SkillSet, static opt => opt.MapFrom(static src => CleanSkillSet(src.SkillSet)))
            // .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            // .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.ActionStatus))
            // .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
            // .ForMember(dest => dest.IsFresher, opt => opt.MapFrom(src => src.IsFresher))
            // //.ForMember(dest => dest.Resume, opt =>opt.MapFrom(src => src.ResumeUpload != null ? Convert.ToBase64String(src.ResumeUpload) : null))
            // .ForMember(dest => dest.Resume, opt => opt.MapFrom(src =>!string.IsNullOrEmpty(src.ResumeUpload) ? Convert.FromBase64String(src.ResumeUpload) : Array.Empty<byte>()))

            // .ForMember(dest => dest.IsBlacklisted, opt => opt.MapFrom(src => false)) // Default value
            // .ForMember(dest => dest.LastUpdatedDateTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        }

    }
}

