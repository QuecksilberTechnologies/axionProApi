using AutoMapper;
using ems.application.DTOs.Module;
using ems.application.Features.ModuleCmd.Commands;
using ems.application.Features.OperationCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Handlers
{

    public class GetAllModuleOperationConfigurationCommandHandler : IRequestHandler<GetAllModuleOperationConfigurationCommand, ApiResponse<List<TenantEnableModuleDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllModuleOperationConfigurationCommandHandler> _logger;

        public GetAllModuleOperationConfigurationCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetAllModuleOperationConfigurationCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<TenantEnableModuleDTO>>> Handle(GetAllModuleOperationConfigurationCommand request, CancellationToken cancellationToken)
        {
            try
            {
               // var tenantId = request.moduleOperationConfigurationRequestDTO.TenantId;
                var tenantId = 118;

                var moduleEntities = await _unitOfWork.TenantModuleConfigurationRepository.GetEnabledModulesWithOperationsAsync(tenantId);

                var moduleDTOs = _mapper.Map<List<TenantEnableModuleDTO>>(moduleEntities);

                return new ApiResponse<List<TenantEnableModuleDTO>>
                {
                    IsSucceeded = true,
                    Message = "Modules with operations fetched successfully.",
                    Data = moduleDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching enabled module-operation configuration for tenantId: {TenantId}", request.ModuleOperationConfigurationRequestDTO.TenantId);

                return new ApiResponse<List<TenantEnableModuleDTO>>
                {
                    IsSucceeded = false,
                    Message = "Failed to fetch enabled module-operation configuration.",
                    Data = null
                };
            }
        }
    }



}
