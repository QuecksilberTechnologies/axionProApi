using AutoMapper;
using ems.application.DTOs.Role;
using ems.application.Features.RoleCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Handlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ApiResponse<RoleResponseDTO>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(
            IRoleRepository roleRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<UpdateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse<RoleResponseDTO>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateRoleCommand for RoleId: {RoleId}", request.updateRoleDTO.Id);

            try
            {
                // Mapping DTO to Entity

                Role roleEntity = _mapper.Map<Role>(request.updateRoleDTO);

                _logger.LogDebug("Mapped UpdateRoleDTO to Role entity: {@RoleEntity}", roleEntity);

                // Update role in the repository
                _logger.LogInformation("Updating role in the repository...");
                Role updatedRoles = await _roleRepository.UpdateRoleAsync(roleEntity);

                // If no roles were updated, return an appropriate message
                if (updatedRoles == null)
                {
                    _logger.LogWarning("No roles were updated for RoleId: {RoleId}", request.updateRoleDTO.Id);
                    return new ApiResponse<RoleResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "No roles were updated.",
                        Data = new RoleResponseDTO()
                    };
                }

                _logger.LogInformation("Successfully updated roles in database. Mapping to DTO...");

                // Map the updated roles to DTOs
                RoleResponseDTO roleDTOs = _mapper.Map<RoleResponseDTO>(updatedRoles);
                _logger.LogDebug("Mapped updated roles to DTO: {@RoleDTOs}", roleDTOs);

                // Return the success response
                _logger.LogInformation("UpdateRoleCommand handled successfully.");
                return new ApiResponse<RoleResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Roles updated successfully",
                    Data = roleDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating roles for RoleId: {RoleId}", request.updateRoleDTO.Id);
                return new ApiResponse<RoleResponseDTO>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
