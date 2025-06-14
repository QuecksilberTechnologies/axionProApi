using AutoMapper;
 
using ems.application.Features.LeaveCmd.Commands;
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
using ems.application.Features.AssetCmd.Commands;
using ems.application.DTOs.Asset;
using Microsoft.Extensions.Logging;

namespace ems.application.Features.AssetCmd.Handlers
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, ApiResponse<AssetResponseDTO>>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
         private readonly ILogger<UpdateAssetCommand> _logger; // यदि logger का उपयोग करना हो

        public UpdateAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<UpdateAssetCommand> logger)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<AssetResponseDTO>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO से Asset में mapping
                 Asset existingAssetValue = _mapper.Map<Asset>(request.updateAssetDTO);
                
                existingAssetValue.UpdatedDateTime = DateTime.Now;
                
                // Asset को update करना
                Asset assets = await _assetRepository.UpdateAssetAsync(existingAssetValue);

                if (assets == null )
                {
                    return new ApiResponse<AssetResponseDTO>
                    {
                        IsSucceeded = false,
                        Message = "No asset was updated.",
                        Data = null
                    };
                }

                // Updated Asset list को DTO में map करना
                AssetResponseDTO allAssetDTOs = _mapper.Map<AssetResponseDTO>(assets);

                return new ApiResponse<AssetResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Asset updated successfully.",
                    Data = allAssetDTOs
                };
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error occurred while updating asset.");
                return new ApiResponse<AssetResponseDTO>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }


}