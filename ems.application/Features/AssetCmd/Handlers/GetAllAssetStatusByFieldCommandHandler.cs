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
    public class GetAllAssetStatusByFieldCommandHandler : IRequestHandler<GetAllAssetStatusByFieldCommand, ApiResponse<List<AssetStatusResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllAssetStatusByFieldCommandHandler> _logger;

        public GetAllAssetStatusByFieldCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllAssetStatusByFieldCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<AssetStatusResponseDTO>>> Handle(GetAllAssetStatusByFieldCommand request, CancellationToken cancellationToken)
        {
            try
            {

                // Step 1: Map request DTO to AssetStatus entity
                AssetStatus asset = _mapper.Map<AssetStatus>(request.assetStatusRequestDTO);

                // Step 2: Fetch filtered data from repository
                List<AssetStatus> assetStatusList = await _unitOfWork.AssetRepository.GetAllAssetStatusByTenantAsync(asset);

                if (assetStatusList.Count == 0) 
                {
                    return new ApiResponse<List<AssetStatusResponseDTO>>
                    {
                        IsSucceeded = false,
                        Message = "Asset statuses not found!.",
                        Data = null
                    };

                }

                // Step 3: Map domain list to response DTO list
                List<AssetStatusResponseDTO> allAssetStatusResponseDTOs = _mapper.Map<List<AssetStatusResponseDTO>>(assetStatusList);


                // Step 4: Logging
                _logger.LogInformation("Successfully retrieved {Count} asset status records.", allAssetStatusResponseDTOs.Count);


                // Step 5: Return wrapped response
                return new ApiResponse<List<AssetStatusResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "Asset statuses fetched successfully.",
                    Data = allAssetStatusResponseDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching asset status records.");

                return new ApiResponse<List<AssetStatusResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "An error occurred while fetching asset statuses.",
                    Data = null
                };
            }
        }
    }
}