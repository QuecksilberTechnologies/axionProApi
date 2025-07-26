using AutoMapper;
using ems.application.DTOs.Module;
using ems.application.Features.ModuleCmd.Commands;
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

namespace ems.application.Features.ModuleCmd.Handlers
{
    public class CreateModuleOperationMappingByProductOwnerCommandHandler :IRequestHandler<CreateModuleOperationMappingByProductOwnerCommand, ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>>
    {
        private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateModuleOperationMappingByProductOwnerCommandHandler> _logger;

    public CreateModuleOperationMappingByProductOwnerCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<CreateModuleOperationMappingByProductOwnerCommandHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

        public async Task<ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>> Handle(CreateModuleOperationMappingByProductOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var addedModule = await _unitOfWork.ModuleOperationMappingRepository
                    .SaveModuleOperationMappingsAsync(request.dto);

                if (addedModule == null)
                {
                    _logger.LogWarning("Failed to save module-operation mapping.");
                    return new ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Failed to create module mapping.",
                        Data = null
                    };
                }

                _logger.LogInformation("Module mapping created successfully for ModuleId {ModuleId}", addedModule.ModuleId);

                return new ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Module mapping created successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating module mapping.");
                return new ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "Failed to create module mapping.",
                    Data = null
                };
            }
        }

    }
}
 