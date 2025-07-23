using AutoMapper;
using ems.application.DTOs.Asset;
 
using ems.application.Features.AssetCmd.Commands;
using ems.application.Features.UserLoginAndDashboardCmd.Commands;
using ems.application.Features.UserLoginAndDashboardCmd.Handlers;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.AssetCmd.Handlers
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, ApiResponse<List<AssetResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAssetCommandHandler> _logger;

        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreateAssetCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<AssetResponseDTO>>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.dto == null)
                {
                    return new ApiResponse<List<AssetResponseDTO>>
                    {
                        IsSucceeded = false,
                        Message = "Invalid request or missing asset creation.",
                        Data = null
                    };
                }                


                // Map CreateAssetDTO to Asset entity
                Asset asset = _mapper.Map<Asset>(request.dto);
                asset.AddedById = request.dto.EmployeeId;
                // Duplicate check
                bool isAssetDuplicate = await _unitOfWork.AssetRepository.IsAssetDuplicate(asset);
                if (isAssetDuplicate)
                {
                    return new ApiResponse<List<AssetResponseDTO>>
                    {
                        IsSucceeded = false,
                        Message = "Asset already exists.",
                        Data = null
                    };
                }

                // Add asset and fetch updated asset list
                List<Asset> assets = await _unitOfWork.AssetRepository.AddAssetAsync(asset);                
                //List<AssetType> assetTypes = await _unitOfWork.AssetRepository.GetAllAssetTypeAsync(asset.TenantId, asset.IsActive);
                //List<AssetStatus> assetsStatus = await _unitOfWork.AssetRepository.GetAllAssetsStatus(asset.TenantId, asset.IsActive);
                
                // Map to DTO lists
                  List<AssetResponseDTO> assetDTOs = _mapper.Map<List<AssetResponseDTO>>(assets);
               // List<GetAllAssetTypeDTO> assetTypeDTOs = _mapper.Map<List<GetAllAssetTypeDTO>>(assetTypes);

                // Create composite DTO
                //var compositeDTO = new GetAllAssetWithDependentEntityDTO
                //{
                //    GetAllAssetDTOs = assetDTOs,
                //    GetAllAssetTypeDTOs = assetTypeDTOs
                //};

                // Wrap composite DTO in a list (as per return type)
            //    var resultDTOList = new List<GetAllAssetDTO> { compositeDTO };

                return new ApiResponse<List<AssetResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "Asset created successfully.",
                    Data = assetDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the asset creation request.");
                return new ApiResponse<List<AssetResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "An error occurred while processing the request.",
                    Data = null
                };
            }
        }
    }
}
