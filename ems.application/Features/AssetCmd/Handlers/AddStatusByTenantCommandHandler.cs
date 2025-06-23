using AutoMapper;
using ems.application.DTOs.Asset;
using ems.application.Features.AssetCmd.Commands;
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

namespace ems.application.Features.AssetCmd.Handlers
{
    public class AddStatusByTenantCommandHandler : IRequestHandler<AddStatusByTenantCommand, ApiResponse<AssetStatusResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddStatusByTenantCommandHandler> _logger;

        public AddStatusByTenantCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<AddStatusByTenantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<AssetStatusResponseDTO>> Handle(AddStatusByTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO ko Entity mein map karein

    
                var assetStatus = _mapper.Map<AssetStatus>(request.assetStatusRequestDTO);
                 
                  

               AssetStatus assetsList = await _unitOfWork.AssetRepository.AddAssetStatusByTenantAsync(assetStatus);             
                // Entity list to DTO list
                var resultDTOList = _mapper.Map<AssetStatusResponseDTO>(assetsList);
                return new ApiResponse<AssetStatusResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Asset Status created successfully.",
                    Data = resultDTOList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding asset status.");
                return new ApiResponse<AssetStatusResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "Something went wrong while adding asset status.",
                    Data = null
                };
            }
        }
    }

}