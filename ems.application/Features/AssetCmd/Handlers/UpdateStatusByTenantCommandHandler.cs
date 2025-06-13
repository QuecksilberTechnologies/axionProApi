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
    public class UpdateStatusByTenantCommandHandler : IRequestHandler<UpdateStatusByTenantCommand, ApiResponse<AllAssetStatusResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateStatusByTenantCommandHandler> _logger;

        public UpdateStatusByTenantCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<UpdateStatusByTenantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<AllAssetStatusResponseDTO>> Handle(UpdateStatusByTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO ko Entity mein map karein

                request.assetStatusRequestDTO.IsActive = request.assetStatusRequestDTO.IsActive ?? false;
                var assetStatus = _mapper.Map<AssetStatus>(request.assetStatusRequestDTO);
                assetStatus.UpdatedDateTime = DateTime.Now;
                AssetStatus assetsList = await _unitOfWork.AssetRepository.UpdateAssetStatusByTenantAsync(assetStatus);
                // Entity list to DTO list
                var resultDTOList = _mapper.Map<AllAssetStatusResponseDTO>(assetsList);
                return new ApiResponse<AllAssetStatusResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Asset Status updated successfully.",
                    Data = resultDTOList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Updateing asset status.");
                return new ApiResponse<AllAssetStatusResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "Something went wrong while Updateing asset status.",
                    Data = null
                };
            }
        }
    }

}
