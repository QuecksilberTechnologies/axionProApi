using AutoMapper;
using ems.application.DTOs.Asset;
using ems.application.Features.AssetCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ems.application.Features.AssetCmd.Handlers
{
    public class UpdateAssetTypeCommandHandler : IRequestHandler<UpdateAssetTypeCommand, ApiResponse<AssetTypeResponseDTO>>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAssetTypeCommand> _logger; // यदि logger का उपयोग करना हो

        public UpdateAssetTypeCommandHandler(IAssetRepository assetRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<AssetTypeResponseDTO>> Handle(UpdateAssetTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Step 1: Map DTO to Entity AssetTypeRequestDTO
                AssetType asset = _mapper.Map<AssetType>(request.updateAssetTypeRequest);

                // Step 2: Call repository to update
                AssetType? updatedAsset = await _assetRepository.UpdateAssetTypeByTenantAsync(asset);

                // Step 3: Handle null response (if update failed or not found)
                if (updatedAsset == null)
                {
                    return new ApiResponse<AssetTypeResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "Asset type not found or update failed.",
                        Data = null
                    };
                }

                // Step 4: Map updated entity to response DTO
                AssetTypeResponseDTO responseDTO = _mapper.Map<AssetTypeResponseDTO>(updatedAsset);

                return new ApiResponse<AssetTypeResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Asset updated successfully.",
                    Data = responseDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset.");

                return new ApiResponse<AssetTypeResponseDTO>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }


}
