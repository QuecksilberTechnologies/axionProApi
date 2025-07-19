using AutoMapper;
using ems.application.DTOs.Department;
using ems.application.Features.DepartmentCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ems.application.Features.DepartmentCmd.Handlers
{
    public class GetAllActiveDepartmentQueryHandler : IRequestHandler<GetAllActiveDepartmentQuery, ApiResponse<List<GetAllDepartmentResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllActiveDepartmentQueryHandler> _logger;

        public GetAllActiveDepartmentQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetAllActiveDepartmentQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetAllDepartmentResponseDTO>>> Handle(GetAllActiveDepartmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAllActiveAsync(request.Dto.TenantId);

                var departmentDTOs = _mapper.Map<List<GetAllDepartmentResponseDTO>>(departments);

                _logger.LogInformation("Successfully retrieved {Count} active departments.", departmentDTOs.Count);

                return new ApiResponse<List<GetAllDepartmentResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "Departments fetched successfully.",
                    Data = departmentDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching active departments.");

                return new ApiResponse<List<GetAllDepartmentResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "Failed to fetch departments.",
                    Data = null
                };
            }
        }
    }
}
