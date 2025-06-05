using AutoMapper;
 
using ems.application.DTOs.Role;
using ems.application.Features.CategoryCmd.Command;
using ems.application.Features.EmployeeCmd.Commands;
using ems.application.Features.RoleCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ApiResponse<List<GetAllRoleDTO>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<List<GetAllRoleDTO>>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Role roleEntity = _mapper.Map<Role>(request.createRoleDTO);
                List<Role> createdRoles = await _roleRepository.CreateRoleAsync(roleEntity);

                if (createdRoles == null || !createdRoles.Any())
                {
                    return new ApiResponse<List<GetAllRoleDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No roles were created.",
                        Data = new List<GetAllRoleDTO>()
                    };
                }

                List<GetAllRoleDTO> roleDTOs = _mapper.Map<List<GetAllRoleDTO>>(createdRoles);

                return new ApiResponse<List<GetAllRoleDTO>>
                {
                    IsSucceeded = true,
                    Message = "Role created successfully",
                    Data = roleDTOs
                };
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "Error occurred while creating role.");
                return new ApiResponse<List<GetAllRoleDTO>>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }



}
