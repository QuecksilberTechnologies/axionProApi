﻿using AutoMapper;
using ems.application.DTOs.UserLogin;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Constants;
using FluentValidation;
using ems.application.DTOs.BasicAndRoleBaseMenuDTO;
using Microsoft.Extensions.Logging;

namespace ems.application.Features.UserLoginAndDashboardCmd.Handlers
{
    public class AttendanceRequestHandler : IRequestHandler<EmployeeTypeBasicMenuCommand, ApiResponse<AccessDetailResponseDTO>>
    {
        private readonly IEmployeeTypeBasicMenuRepository employeeTypeBasicMenuRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
   
        
        public AttendanceRequestHandler(IEmployeeTypeBasicMenuRepository employeeTypeBasicMenuRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.employeeTypeBasicMenuRepository = employeeTypeBasicMenuRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
    
        }

       
        public async  Task<ApiResponse<AccessDetailResponseDTO>> Handle(EmployeeTypeBasicMenuCommand? request, CancellationToken cancellationToken)
        {
           
            // Fetch the basic menus for the given employee type and platform
            var basicMenuDTOs = await employeeTypeBasicMenuRepository.GetBasicMenuDTO(request.AccessDetailDTO.EmployeeTypeId, request.AccessDetailDTO.ForPlatform);

             // Create the AccessDetailResponseDTO object and bind the fetched menus
            var accessDetailResponse = new AccessDetailResponseDTO
            {
                EmployeeId = request.AccessDetailDTO.EmployeeId,  // Assuming EmployeeId is passed in the request
                ForPlatform = request.AccessDetailDTO.ForPlatform,  // Assuming this is for the platform value 2 (mobile or web)
                BasicMenus = basicMenuDTOs
            };

            // Construct the API response
            var apiResponse = new ApiResponse<AccessDetailResponseDTO>
            {
                IsSuccecced = ConstantValues.isSucceeded,  // Indicating the operation succeeded
                Message = "Menus fetched successfully.",
                Data = accessDetailResponse // Bind the AccessDetailResponseDTO
            };

            // Log the successful operation
           // logger?.LogInformation("Access detail response created successfully for EmployeeId: {EmployeeId}, Platform: {ForPlatform}",
             //   request.AccessDetailDTO.EmployeeId, 2);

            // Return the response (example for a Web API method)
            return apiResponse;
 



        }
    }
}
