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

namespace ems.application.Features.AssetCmd.Handlers
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, ApiResponse<List<GetAllAssetWithDependentEntityDTO>>>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        // private readonly ILogger<UpdateAssetCommandHandler> _logger; // यदि logger का उपयोग करना हो

        public UpdateAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<GetAllAssetWithDependentEntityDTO>>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO से Asset में mapping
                Asset asset = _mapper.Map<Asset>(request.updateAssetDTO);

                // Asset को update करना
                List<Asset> assets = await _assetRepository.UpdateAssetAsync(asset);

                if (assets == null || !assets.Any())
                {
                    return new ApiResponse<List<GetAllAssetWithDependentEntityDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No asset was updated.",
                        Data = null
                    };
                }

                // Updated Asset list को DTO में map करना
                List<GetAllAssetWithDependentEntityDTO> allAssetDTOs = _mapper.Map<List<GetAllAssetWithDependentEntityDTO>>(assets);

                return new ApiResponse<List<GetAllAssetWithDependentEntityDTO>>
                {
                    IsSucceeded = true,
                    Message = "Asset updated successfully.",
                    Data = allAssetDTOs
                };
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error occurred while updating asset.");
                return new ApiResponse<List<GetAllAssetWithDependentEntityDTO>>
                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }
    }


}