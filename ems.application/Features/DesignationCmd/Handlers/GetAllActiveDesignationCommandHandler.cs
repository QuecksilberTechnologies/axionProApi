using AutoMapper;
using ems.application.DTOs.Designation;
using ems.application.Features.DesignationCmd.Commands;
using ems.application.Features.DesignationCmd.Queries;
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

namespace ems.application.Features.DesignationCmd.Handlers
{
    public class GetAllActiveDesignationCommandHandler :IRequestHandler<GetAllActiveDesignationCommand, ApiResponse<List<GetAllDesignationDTO>>>
    {
        private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllActiveDesignationCommandHandler> _logger;

    public GetAllActiveDesignationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllActiveDesignationCommandHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse<List<GetAllDesignationDTO>>> Handle(GetAllActiveDesignationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 🔍 Validate TenantId
            if (request.dto.TenantId == null || request.dto.TenantId <= 0)
            {
                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSucceeded = false,
                    Message = "Tenant Id should be provided.",
                    Data = null
                };
            }
            List<Designation> designations = await _unitOfWork.DesignationRepository
                .GetAllActiveDesignationAsync(request.dto.TenantId);

            // 🔍 Filter on IsActive and IsSoftDeleted if needed            

            if (designations == null || !designations.Any())
            {
                _logger.LogWarning("No designations found for TenantId {TenantId}.", request.dto.TenantId);

                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSucceeded = false,
                    Message = "No designations found.",
                    Data = null
                };
            }

            // 🔁 Map to DTOs
            List<GetAllDesignationDTO> getAllDesignationDTOs = _mapper.Map<List<GetAllDesignationDTO>>(designations);

            _logger.LogInformation("Successfully retrieved {Count} designations.", getAllDesignationDTOs.Count);

            return new ApiResponse<List<GetAllDesignationDTO>>
            {
                IsSucceeded = true,
                Message = "Designations fetched successfully.",
                Data = getAllDesignationDTOs
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching designations.");

            return new ApiResponse<List<GetAllDesignationDTO>>
            {
                IsSucceeded = false,
                Message = "An error occurred while fetching designations.",
                Data = null
            };
        }
    }


}
}
