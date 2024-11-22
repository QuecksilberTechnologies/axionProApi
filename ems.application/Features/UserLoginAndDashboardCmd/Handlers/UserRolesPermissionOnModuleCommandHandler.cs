using AutoMapper;
using ems.application.Constants;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using ems.application.DTOs.RoleDTO;
using FluentValidation;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class UserRolesPermissionOnModuleCommandHandler : IRequestHandler<UserRolesPermissionOnModuleCommand, ApiResponse<IEnumerable<UserRolesPermissionOnModuleDTO>>>
    {
        private readonly IUserRolesPermissionOnModuleRepository userRolesPermissionOnModuleRepository; // Add repository here
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserRolesPermissionOnModuleCommandHandler(IUserRolesPermissionOnModuleRepository userRolesPermissionOnModuleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userRolesPermissionOnModuleRepository = userRolesPermissionOnModuleRepository; // Initialize repository
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<UserRolesPermissionOnModuleDTO>>> Handle(UserRolesPermissionOnModuleCommand? request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the request
                if (request == null || request.AccessDetailDTO == null)
                {
                    return new ApiResponse<IEnumerable<UserRolesPermissionOnModuleDTO>>
                    {
                        IsSuccecced = false,
                        Message = "Invalid request or missing AccessDetailDTO."
                    };
                }

                // Fetch the basic menus for the given employee type and platform
                IEnumerable<UserRolesPermissionOnModuleDTO> userRolesPermissionOnModuleDTOs = await userRolesPermissionOnModuleRepository
                    .GetModuleListAndOperationByRollIdAsync(request.AccessDetailDTO.roleInfo.ToList(), request.AccessDetailDTO.ForPlatform);

                // Construct the API response
                var apiResponse = new ApiResponse<IEnumerable<UserRolesPermissionOnModuleDTO>>
                {
                    IsSuccecced = ConstantValues.isSucceeded,  // Indicating the operation succeeded
                    Message = "Menus fetched successfully.",
                    Data = userRolesPermissionOnModuleDTOs // Return the fetched data as IEnumerable
                };

                // Log the successful operation
                // logger?.LogInformation("Access detail response created successfully for EmployeeId: {EmployeeId}, Platform: {ForPlatform}",
                //   request.AccessDetailDTO.EmployeeId, request.AccessDetailDTO.ForPlatform);

                return apiResponse;
            }
            catch (Exception ex)
            {
                // Log the error
                // logger?.LogError(ex, "An error occurred while processing the request.");

                // Return a failure response
                return new ApiResponse<IEnumerable<UserRolesPermissionOnModuleDTO>>
                {
                    IsSuccecced = false,
                    Message = "An error occurred while processing the request. Please try again later."
                };
            }
        }
    }
}
