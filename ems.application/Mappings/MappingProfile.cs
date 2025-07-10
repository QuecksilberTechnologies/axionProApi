using AutoMapper;
using ems.application.DTOs.Asset;
 
using ems.application.DTOs.Category;
using ems.application.DTOs.Client;
using ems.application.DTOs.Department;
using ems.application.DTOs.Designation;
using ems.application.DTOs.EmailTemplate;
using ems.application.DTOs.Employee;
using ems.application.DTOs.Leave;
using ems.application.DTOs.Module;
using ems.application.DTOs.Operation;
using ems.application.DTOs.Region;
using ems.application.DTOs.Registration;
using ems.application.DTOs.Role;
using ems.application.DTOs.SubscriptionModule;
using ems.application.DTOs.Tenant;
using ems.application.DTOs.TenantIndustry;
using ems.application.DTOs.Transport;
using ems.application.DTOs.UserLogin;
using ems.application.DTOs.UserRole;
using ems.domain.Entity;

using FluentValidation;
using MediatR;
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

            CreateMap<CreateAssetRequestByTenantAdminDTO, Asset>();
            //   CreateMap<Asset, GetAllAssetWithDependentEntityDTO>();


            CreateMap<AssetType, GetAllAssetTypeDTO>();

            CreateMap<Asset, AssetResponseDTO>();
            //.ForMember(dest => dest.WarrantyExpiryDate,
            //           opt => opt.MapFrom(src => src.WarrantyExpiryDate.HasValue
            //                                       ? DateOnly.FromDateTime(src.WarrantyExpiryDate.Value)
            //                                       : default));

            CreateMap<Asset, GetAssetRequestByTenantAdminDTO>().ReverseMap();


            CreateMap<SubscriptionPlanResponseDTO, SubscriptionPlan>().ReverseMap();



            CreateMap<AssetResponseDTO, Asset>().ReverseMap();


            CreateMap<UpdateAssetRequestDTO, Asset>().ReverseMap();

            CreateMap<DeleteAssetRequestDTO, Asset>().ReverseMap();

            CreateMap<AssetStatus, AssetStatusRequestDTO>().ReverseMap();

            CreateMap<AssetStatusResponseDTO, AssetStatus>().ReverseMap();


            CreateMap<AssetType, AssetTypeRequestDTO>().ReverseMap();
            CreateMap<AssetType, AssetTypeResponseDTO>().ReverseMap(); // 🔥 Yeh zaroori hai


            //   CreateMap<AssetType, AssetTypeResponseDTO>().ReverseMap(); // 🔥 Yeh zaroori hai

            CreateMap<AssetType, UpdateAssetTypeRequestDTO>().ReverseMap();


            CreateMap<AssetType, DeleteAssetTypeRequestDTO>().ReverseMap();


            CreateMap<AssetStatus, UpdateAssetStatusRequestDTO>().ReverseMap();
            CreateMap<UpdateAssetStatusRequestDTO, AssetStatus>().ReverseMap();


            CreateMap<AssetStatus, DeleteAssetStatusRequestDTO>().ReverseMap();



            CreateMap<domain.Entity.Module, CommonItemDTO>().ReverseMap();




            CreateMap<CreateDesignationDTO, Designation>();
            CreateMap<Designation, GetAllDesignationDTO>();
            CreateMap<UpdateDesignationDTO, Designation>();

            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<Department, GetAllDepartmentDTO>();
            CreateMap<UpdateDepartmentDTO, Department>();



            CreateMap<CreateSubModuleRequestDTO, domain.Entity.Module>();
            CreateMap<CreateModuleRequestDTO, domain.Entity.Module>().ReverseMap();
            CreateMap<ModuleResponseDTO, domain.Entity.Module>();






            //CreateMap<CreateOperationByProductOwnerRequestDTO, Operation>();
            //CreateMap<Operation, GetAllOperationDTO>();
            //CreateMap<UpdateOperationByProductOwnerRequestDTO, Operation>();

          

        CreateMap<CreateClientTypeDTO, ClientType>();
            CreateMap<ClientType, GetAllClientTypeDTO>();
            CreateMap<UpdateClientTypeDTO, ClientType>();  // ✅ Yeh likhna hoga!


            CreateMap<CreateTravelModeDTO, TravelMode>();
            CreateMap<TravelMode, GetAllTravelModeDTO>();
            CreateMap<UpdateTravelModeDTO, TravelMode>();


            CreateMap<CreateLeaveTypeDTO, LeaveType>();
            CreateMap<LeaveType, GetAllLeaveTypeDTO>();
            CreateMap<UpdateLeaveTypeDTO, LeaveType>();  // ✅ Yeh likhna hoga!

            // Map Employee to EmployeeDTO
              CreateMap<Employee, EmployeeDTO>();

            CreateMap<GetRoleIdByRoleCodeRequestDTO,Role>().ReverseMap();  //  






               CreateMap< CreateRoleDTO,Role > ()
                  .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => src.EmployeeId)) ; // Example

            CreateMap<UpdateRoleDTO, Role>()
                .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => src.EmployeeId));


            // Role Entity to GetAllRoleDTO Mapping
            // Direct entity to DTO mapping
            CreateMap<GetActiveRoleRequestDTO,Role >()

                       .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));// Example

            CreateMap<GetRoleRequestDTO, Role>();

                  



            CreateMap<RoleResponseDTO, Role>().ReverseMap();

            //   CreateMap<TenantCreateResponseDTO, TenantIndustry>();
            CreateMap<TenantIndustry, TenantIndustryResponseDTO>();



            CreateMap<UserRole, UserRoleDTO>()
                          .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName)) // Example
                          .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive == true));

            // CreateRoleDTO to Role Mapping (for creating roles)




            // Agar reverse mapping chahiye toh, isse bhi add kar sakte hain
            CreateMap<EmployeeDTO, Employee>();
                CreateMap<EmployeeLoginInfoDTO, LoginResponseDTO>().ForMember(dest => dest.EmployeeInfo, opt => opt.MapFrom(src => src));
                CreateMap<Category, CategoryResponseDTO>();

            CreateMap<Employee, EmployeeLoginInfoDTO>()
                  .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                        // `Employee.Id` ➝ `EmployeeInfoDTO.EmployeeId`

                            .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src =>
                                  $"{src.FirstName} {(string.IsNullOrWhiteSpace(src.MiddleName) ? "" : src.MiddleName + " ")}{src.LastName}".Trim()))
                                   // `Employee.FirstName + MiddleName + LastName` ➝ `EmployeeInfoDTO.EmployeeFullName`

                                          .ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.EmployeeTypeId))
                                               // `Employee.EmployeeTypeId` ➝ `EmployeeInfoDTO.EmployeeTypeId`

                                            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId.HasValue ? src.DepartmentId : null))
                                             // `Employee.DepartmentId` ➝ `EmployeeInfoDTO.DepartmentId` (Nullable check)

                                       .ForMember(dest => dest.DesignationId, opt => opt.MapFrom(src => src.DesignationId.HasValue ? src.DesignationId : null))
                                     // `Employee.DesignationId` ➝ `EmployeeInfoDTO.DesignationId` (Nullable check)

                               .ForMember(dest => dest.OfficialEmail, opt => opt.MapFrom(src => src.OfficialEmail ?? null));
            // `Employee.OfficialEmail` ➝ `EmployeeInfoDTO.OfficialEmail`


            // Add mapping for List<Role> to List<GetAllRoleDTO>
            // CreateMap<List<Role>, List<GetAllRoleDTO>>();  // Add this line

            // CreateMap<List<Role>, List<GetAllRoleDTO>>()
            //.ForMember(dest => dest, opt => opt.MapFrom(src => src.Select(x => new GetAllRoleDTO { /* Manually map properties here */ }).ToList()));


            // Mapping Employee entity to LoginEmployeeInfoDTO
            //CreateMap<Employee, EmployeeInfoDTO>()
            //    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id)) // Map EmployeeId
            //    .ForMember(dest => dest.LoginId, opt => opt.MapFrom((src, dest, _, context) => context.Items["LoginId"] as string)) // Map LoginId from context
            //                                                                                                                        //  .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName)) // Map UserName
            //                                                                                                                        //.ForMember(dest => dest.EmployeeMiddleName, opt => opt.MapFrom(src => src.MiddleName)) // Map UserName
            //                                                                                                                        // .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName)) // Map UserName
            //    .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => ((src.FirstName) + src.MiddleName) + src.LastName));
            //    // Map UserName
            //.ForMember(dest => dest.EmployeeTypeId, opt => opt.MapFrom(src => src.EmployeeTypeId.ToString())) // Map EmployeeTypeId
            // .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployementType.ToString())) // Map EmployeeTypeId
            // .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType.TypeName)) // Map EmployeeType Name
            //.ForMember(dest => dest.EmployeeAssignedRoles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => new RoleInfoDTO
            //{
            //    Id = ur.RoleId.GetValueOrDefault(),  // ✅ Fix: Converts nullable int? to int (Defaults to 0 if null)
            //    RoleName = ur.Role.RoleName,
            //    Description = ur.Remark
            //})));

            //CreateMap<EmailTemplate, EmailTemplateDTO>();
            CreateMap<EmailTemplate, EmailTemplateDTO>().ReverseMap();
            CreateMap<LoginCredential, SetLoginPasswordRequestDTO>().ReverseMap();

            CreateMap<Tenant, DTOs.Registration.TenantRequestDTO>().ReverseMap();
            CreateMap<LoginCredential, Employee>().ReverseMap();
            CreateMap<Country, GetAllCountryDTO>().ReverseMap();
            CreateMap<Tenant, DTOs.Tenant.TenantRequestDTO>().ReverseMap(); 
            CreateMap<TenantResponseDTO, Tenant>().ReverseMap();

            CreateMap<SubscriptionPlan, SubscriptionPlanResponseDTO>()
               .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.PlanModuleMappings
               .Where(pmm => pmm.Module != null)
                .Select(pmm => new ModuleResponseDTO
                   {
                   ModuleId = pmm.Module.Id,
                   ModuleName = pmm.Module.ModuleName,
                       ParentModuleId = pmm.Module.ParentModuleId
                 }).ToList()));

                     



            CreateMap<OperationResponseDTO, TenantEnabledOperation>()
               .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => true))
              .ForMember(dest => dest.OperationId, opt => opt.MapFrom(src => src.OperationId)).ForAllMembers(opt => opt.Ignore());

            CreateMap<TenantSubscriptionPlanRequestDTO, TenantSubscription>().ReverseMap();
            CreateMap<TenantSubscriptionPlanResponseDTO, TenantSubscription>().ReverseMap();


            CreateMap<UpdateModuleOperationMappingByProductOwnerRequestDTO, ModuleOperationMapping>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ModuleOperationMappingId))
         .ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.PageURL))
         .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.IconURL))
         .ForMember(dest => dest.IsCommonItem, opt => opt.MapFrom(src => src.IsCommonItem))
         .ForMember(dest => dest.IsOperational, opt => opt.MapFrom(src => src.IsOperational))
         .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
         .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
         .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
         .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => src.ProductOwnerId));

            CreateMap<ModuleOperationMapping, ModuleOperationMappingByProductOwnerResponseDTO>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
    .ForMember(dest => dest.OperationIds, opt => opt.MapFrom(src => new List<int> { src.OperationId }))
    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
    .ForMember(dest => dest.PageURL, opt => opt.MapFrom(src => src.PageUrl))
    .ForMember(dest => dest.IconURL, opt => opt.MapFrom(src => src.IconUrl))
    .ForMember(dest => dest.IsCommonItem, opt => opt.MapFrom(src => src.IsCommonItem ?? false))
    .ForMember(dest => dest.IsOperational, opt => opt.MapFrom(src => src.IsOperational ?? false))
    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority ?? 0))
    .ForMember(dest => dest.Remark, opt => opt.MapFrom(src => src.Remark))
    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
    .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => src.AddedById ?? 0))
    .ForMember(dest => dest.AddedDateTime, opt => opt.MapFrom(src => src.AddedDateTime ?? DateTime.MinValue))
    .ForMember(dest => dest.UpdatedById, opt => opt.MapFrom(src => src.UpdatedById))
    .ForMember(dest => dest.UpdatedDateTime, opt => opt.MapFrom(src => src.UpdatedDateTime));
 
 



            CreateMap<CandidateRequestDTO, Candidate>()
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
         .ForMember(dest => dest.Pan, opt => opt.MapFrom(src => src.Pan))
         .ForMember(dest => dest.Aadhaar, opt => opt.MapFrom(src => src.Aadhaar))
         .ForMember(dest => dest.CandidateReferenceCode, opt => opt.MapFrom(src => src.CandidateReferenceCode))
         .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.ResumeUrl))
         .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.ExperienceYears))
         .ForMember(dest => dest.CurrentLocation, opt => opt.MapFrom(src => src.CurrentLocation))
         .ForMember(dest => dest.ExpectedSalary, opt => opt.MapFrom(src => src.ExpectedSalary))
         .ForMember(dest => dest.CurrentCompany, opt => opt.MapFrom(src => src.CurrentCompany))
         .ForMember(dest => dest.NoticePeriod, opt => opt.MapFrom(src => src.NoticePeriod))
         .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
         .ForMember(dest => dest.AppliedDate, opt => opt.MapFrom(src => src.AppliedDate))
         .ForMember(dest => dest.SkillSet, static opt => opt.MapFrom(static src => CleanSkillSet(src.SkillSet)))
         .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
         .ForMember(dest => dest.ActionStatus, opt => opt.MapFrom(src => src.ActionStatus))
         .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
         .ForMember(dest => dest.IsFresher, opt => opt.MapFrom(src => src.IsFresher))
         //.ForMember(dest => dest.Resume, opt =>opt.MapFrom(src => src.ResumeUpload != null ? Convert.ToBase64String(src.ResumeUpload) : null))
         .ForMember(dest => dest.Resume, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ResumeUpload) ? Convert.FromBase64String(src.ResumeUpload) : Array.Empty<byte>()))

         .ForMember(dest => dest.IsBlacklisted, opt => opt.MapFrom(src => false)) // Default value
         .ForMember(dest => dest.LastUpdatedDateTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        }


    }
}

