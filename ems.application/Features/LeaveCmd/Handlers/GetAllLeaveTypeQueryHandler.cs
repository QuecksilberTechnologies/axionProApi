using AutoMapper;
using ems.application.DTOs.Leave;
 
using ems.application.Features.LeaveCmd.Queries;
using ems.application.Features.RoleCmd.Handlers;
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

namespace ems.application.Features.LeaveCmd.Handlers
{
    public class GetAllLeaveTypeQueryHandler  : IRequestHandler<GetAllLeaveTypeQuery, ApiResponse<List<GetAllLeaveTypeDTO>>>
    {
        private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllLeaveTypeQueryHandler> _logger;

    public GetAllLeaveTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllLeaveTypeQueryHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
        
        public async Task<ApiResponse<List<GetAllLeaveTypeDTO>>> Handle(GetAllLeaveTypeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // ✅ Correcting the method call
            List<LeaveType> roles = await _unitOfWork.LeaveRepository.GetAllLeaveAsync();

            //if (roles == null || !roles.Any())
            //{
            //    _logger.LogWarning("No roles found.");
            //    return new ApiResponse<List<GetAllRoleDTO>>(false, "No roles found", new List<GetAllRoleDTO>());
            //}

            //// ✅ Map Role entities to DTOs
            var leaveTypeDTOs = _mapper.Map<List<GetAllLeaveTypeDTO>>(roles);

            _logger.LogInformation("Successfully retrieved {Count} roles.", leaveTypeDTOs.Count);
            return new ApiResponse<List<GetAllLeaveTypeDTO>>
            {
                IsSuccecced = true,
                Message = "Categories fetched successfully.",
                Data = leaveTypeDTOs
            };






        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching roles.");
            return new ApiResponse<List<GetAllLeaveTypeDTO>>
            {
                IsSuccecced = false,
                Message = "Categories fetched successfully.",
                Data = null
            };
        }

    }

       
    }
}
    