using AutoMapper;
using ems.application.DTOs.Designation;
 
using ems.application.Features.DesignationCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.DesignationCmd.Handlers
{
    public class GetAllDesignationQueryHandler: IRequestHandler<GetAllDesignationQuery, ApiResponse<List<GetAllDesignationDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllDesignationQueryHandler> _logger;

        public GetAllDesignationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllDesignationQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetAllDesignationDTO>>> Handle(GetAllDesignationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 🔍 Validate TenantId
                if (request.designationRequestDTO.TenantId == null || request.designationRequestDTO.TenantId <= 0)
                {
                    return new ApiResponse<List<GetAllDesignationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "Tenant Id should be provided.",
                        Data = null
                    };
                }

                // ✅ Set default IsActive = false if null
                // Option 1: using null-coalescing operator (ternary style)
                //    bool isActive = request.designationRequestDTO.IsActive ? request.designationRequestDTO.IsActive : false;
                bool isActive = request.designationRequestDTO.IsActive ?? true;
               

                // 🔍 Fetch data from repository (you should pass IsActive filter also if required)
                //List<Designation> designations = await _unitOfWork.DesignationRepository
                //    .GetAllDesignationAsync(request.designationRequestDTO.TenantId, isActive);
                List<Designation> designations = await _unitOfWork.DesignationRepository
                    .GetAllDesignationWithDepartmentAsync(request.designationRequestDTO.TenantId, isActive,
                    request.designationRequestDTO.DepartmentId);

                // 🔍 Filter on IsActive and IsSoftDeleted if needed
                //designations = designations
                //    .Where(d => d.IsActive == request.designationRequestDTO.IsActive && d.IsSoftDeleted == false)
                //    .ToList();

                if (designations == null || !designations.Any())
                {
                    _logger.LogWarning("No designations found for TenantId {TenantId}.", request.designationRequestDTO.TenantId);

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
