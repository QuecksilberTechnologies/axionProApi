using AutoMapper;
using ems.application.Features.SubscriptionCmd.Commands;
using ems.application.Features.SubscriptionCmd.Handlers;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Features.TenantCmd.Commands;
using ems.application.DTOs.Tenant;

namespace ems.application.Features.TenantCmd.Handlers
{

    public class GetTenantEnabledModuleOperationCommandHandler : IRequestHandler<GetTenantEnabledModuleOperationCommand, ApiResponse<TenantEnabledModuleOperationsResponseDTO>>
    {
        private readonly ITenantModuleConfigurationRepository _tenantModuleConfigurationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTenantEnabledModuleOperationCommandHandler> _logger;

        public GetTenantEnabledModuleOperationCommandHandler(
            ITenantModuleConfigurationRepository tenantModuleConfigurationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetTenantEnabledModuleOperationCommandHandler> logger)
        {
            _tenantModuleConfigurationRepository = tenantModuleConfigurationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<TenantEnabledModuleOperationsResponseDTO>> Handle(
            GetTenantEnabledModuleOperationCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Request null check
                if (request == null)
                {
                    _logger.LogWarning("GetTenantEnabledModuleOperationCommand is null.");
                    return new ApiResponse<TenantEnabledModuleOperationsResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Request cannot be null.",
                        Data = null
                    };
                }

                // ✅ TenantId validation
                var tenantId = request.TenantEnabledModuleOperationsRequestDTO.TenantId;
                if (tenantId <= 0)
                {
                    _logger.LogWarning("Invalid TenantId: {TenantId}", tenantId);
                    return new ApiResponse<TenantEnabledModuleOperationsResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "TenantId is required and must be greater than 0.",
                        Data = null
                    };
                }

                // ✅ Get data from repository
                var responseDTO = await _unitOfWork.TenantModuleConfigurationRepository.GetEnabledModulesWithOperationsAsync(request.TenantEnabledModuleOperationsRequestDTO);
                   

                return new ApiResponse<TenantEnabledModuleOperationsResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Modules and operations fetched successfully.",
                    Data = responseDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching module operations for tenant.");
                return new ApiResponse<TenantEnabledModuleOperationsResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "Something went wrong while fetching module operations.",
                    Data = null
                };
            }
        }
    }

}

