using AutoMapper;
using ems.application.DTOs.Asset;
using ems.application.Features.AssetCmd.Queries;
using ems.application.Features.DesignationCmd.Handlers;
using ems.application.Features.DesignationCmd.Queries;
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
    public class GetAllAssetQueryHandler :IRequestHandler<GetAllAssetQuery, ApiResponse<List<GetAllAssetWithDependentEntityDTO>>>
    {
        private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllAssetQueryHandler> _logger;

    public GetAllAssetQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllAssetQueryHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

        public async Task<ApiResponse<List<GetAllAssetWithDependentEntityDTO>>> Handle(GetAllAssetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                  


                List<Asset> assets = await _unitOfWork.AssetRepository.GetAllAssetAsync();
                if (assets == null || !assets.Any())
                {
                    _logger.LogWarning("No assets found.");
                    return new ApiResponse<List<GetAllAssetWithDependentEntityDTO>>
                    {
                        IsSuccecced = false,
                        Message = "No assets found.",
                        Data = new List<GetAllAssetWithDependentEntityDTO>()
                    };
                }

                // सभी asset types को fetch करें (assuming एक repository method उपलब्ध है)
                List<AssetType> assetTypes = await _unitOfWork.AssetRepository.GetAllAssetTypeAsync();
                List<AssetStatus> assetsStatus = await _unitOfWork.AssetRepository.GetAssetsStatus();

                // Asset entities को respective DTOs में map करें
                List<GetAllAssetDTO> assetDTOs = _mapper.Map<List<GetAllAssetDTO>>(assets);
                List<GetAllAssetTypeDTO> assetTypeDTOs = _mapper.Map<List<GetAllAssetTypeDTO>>(assetTypes);
                List<GetAllAssetStatusDTO> assetStatusDTOs = _mapper.Map<List<GetAllAssetStatusDTO>>(assetsStatus);

                // Composite DTO तैयार करें
                var compositeDTO = new GetAllAssetWithDependentEntityDTO
                {
                    GetAllAssetDTOs = assetDTOs,
                    GetAllAssetTypeDTOs = assetTypeDTOs,
                    GetAllAssetStatusDTOs = assetStatusDTOs
                };

                // यदि multiple composite DTO objects की जरूरत है, तो आप इसे list में wrap कर सकते हैं।
                // यहाँ हम एक single composite DTO को list में wrap करके return कर रहे हैं।
                var compositeDTOList = new List<GetAllAssetWithDependentEntityDTO> { compositeDTO };

                _logger.LogInformation("Successfully retrieved {AssetCount} asset(s) and {AssetTypeCount} asset type(s).", assetDTOs.Count, assetTypeDTOs.Count);
                return new ApiResponse<List<GetAllAssetWithDependentEntityDTO>>
                {
                    IsSuccecced = true,
                    Message = "Assets fetched successfully.",
                    Data = compositeDTOList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching assets.");
                return new ApiResponse<List<GetAllAssetWithDependentEntityDTO>>
                {
                    IsSuccecced = false,
                    Message = "Error occurred while fetching assets.",
                    Data = null
                };
            }
        }


    }
}
 