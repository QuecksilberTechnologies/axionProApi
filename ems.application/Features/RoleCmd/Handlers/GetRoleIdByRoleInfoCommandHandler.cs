using AutoMapper;
using ems.application.Features.RoleCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RoleCmd.Handlers
{
    public class GetRoleIdByRoleInfoCommandHandler : IRequestHandler<GetRoleIdByRoleInfoCommand, ApiResponse<int>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<GetRoleIdByRoleInfoCommandHandler> _logger;
        private readonly IMapper _mapper;
        public GetRoleIdByRoleInfoCommandHandler(IRoleRepository roleRepository, ILogger<GetRoleIdByRoleInfoCommandHandler> logger, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse<int>> Handle(GetRoleIdByRoleInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.dto;
                Role role  =  _mapper.Map<Role>(dto);
                

                int roleId = await _roleRepository.GetRoleIdByRoleInfoAsync(role);

                if (roleId == 0)
                {
                    _logger.LogWarning("Role not found for RoleCode: {RoleCode}, RoleType: {RoleType}", dto.RoleCode, dto.RoleType);
                    return ApiResponse<int>.Fail("Role not found.");
                }

                return ApiResponse<int>.Success(roleId, "Role ID fetched successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting role ID.");
                return ApiResponse<int>.Fail("An error occurred while fetching role ID.");
            }
        }
    }
}
