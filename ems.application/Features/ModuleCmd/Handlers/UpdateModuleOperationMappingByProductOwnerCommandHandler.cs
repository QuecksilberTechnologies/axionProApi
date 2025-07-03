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
    public class UpdateModuleOperationMappingByProductOwnerCommandHandler : IRequestHandler<UpdateModuleOperationMappingByProductOwnerCommand, ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateModuleOperationMappingByProductOwnerCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateModuleOperationMappingByProductOwnerCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateModuleOperationMappingByProductOwnerCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>> Handle(UpdateModuleOperationMappingByProductOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.dto;

                // 🔍 Validation
                    
                 
                    // ✅ Map response DTO
                var responseDTO = _mapper.Map<ModuleOperationMapping>(request.dto);

                // 🔄 Update logic call to repository
                ModuleOperationMapping result = await _unitOfWork.ModuleOperationMappingRepository.UpdateModuleOperationMappingsAsync(responseDTO);
                
                var resultresponseDTO = _mapper.Map<ModuleOperationMappingByProductOwnerResponseDTO>(result);



                if (resultresponseDTO == null)
                {
                    return new ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Failed to update module-operation mappings.",
                        Data = null
                    };
                }


                return new ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Module-operation mappings updated successfully.",
                    Data = resultresponseDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating module-operation mappings.");
                return new ApiResponse<ModuleOperationMappingByProductOwnerResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "An unexpected error occurred.",
                    Data = null
                };
            }
        }



    }

}
