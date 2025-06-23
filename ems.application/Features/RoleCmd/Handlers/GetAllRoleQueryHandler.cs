
using AutoMapper;
using ems.application.DTOs.Role;
using ems.application.Features.RoleCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Handlers
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, ApiResponse<List<RoleResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllRoleQueryHandler> _logger;

        public GetAllRoleQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllRoleQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<RoleResponseDTO>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {


                // ✅ Correcting the method call
                var role = _mapper.Map<Role>(request.RoleRequest);

                
                List<Role> roles = await _unitOfWork.RoleRepository.GetAllRolesAsync( role);
                 
                //if (roles == null || !roles.Any())
                //{
                //    _logger.LogWarning("No roles found.");
                //    return new ApiResponse<List<GetAllRoleDTO>>(false, "No roles found", new List<GetAllRoleDTO>());
                //}

                //// ✅ Map Role entities to DTOs
                var roleDTOs = _mapper.Map<List<RoleResponseDTO>>(roles);
                
                _logger.LogInformation("Successfully retrieved {Count} roles.", roleDTOs.Count);
                return new ApiResponse<List<RoleResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "Categories fetched successfully.",
                    Data = roleDTOs
                };





              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching roles.");
                return new ApiResponse<List<RoleResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "Categories fetched successfully.",
                    Data = null
                };
            }

        }
    }
}
 

/**/