using AutoMapper;
using ems.application.DTOs.Role;
using ems.application.Features.RoleCmd.Queries;
using ems.application.Interfaces;
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
    public class GetAllRoleSummaryQueryHandler : IRequestHandler<GetAllRoleSummaryQuery, ApiResponse<List<GetRoleSummaryResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllRoleSummaryQueryHandler> _logger;

        public GetAllRoleSummaryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllRoleSummaryQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetRoleSummaryResponseDTO>>> Handle(GetAllRoleSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {


                // ✅ Correcting the method call

               
                List<Role> roles = await _unitOfWork.RoleRepository.GetAllActiveRolesSummaryAsync(request.Dto.TenantId);
                // Remove duplicate roles based on RoleCode
                var distinctRoles = roles
                    .GroupBy(r => r.RoleCode)
                    .Select(g => g.First())
                    .ToList();

                var roleDTOs = _mapper.Map<List<GetRoleSummaryResponseDTO>>(distinctRoles);

                _logger.LogInformation("Successfully retrieved {Count} roles.", roleDTOs.Count);
                return new ApiResponse<List<GetRoleSummaryResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "All tenant-summary fetched successfully.",
                    Data = roleDTOs
                };






            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching roles.");
                return new ApiResponse<List<GetRoleSummaryResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "Error while fetching roles-summary.",
                    Data = null
                };
            }

        }
    }

}