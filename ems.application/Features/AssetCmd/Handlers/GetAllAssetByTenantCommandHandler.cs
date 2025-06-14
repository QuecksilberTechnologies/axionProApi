using AutoMapper;
using ems.application.DTOs.Asset;
using ems.application.Features.AssetCmd.Commands;
using ems.application.Features.AssetCmd.Queries;
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
    public class GetAllAssetByTenantCommandHandler : IRequestHandler<GetAllAssetByTenantCommand, ApiResponse<List<AssetResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllAssetByTenantCommandHandler> _logger;

        public GetAllAssetByTenantCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllAssetByTenantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<AssetResponseDTO>>> Handle(GetAllAssetByTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Request se filter object banayein (agar DTO pass ho raha hai to)
                Asset filterAsset = _mapper.Map<Asset>(request.getAssetTypeRequest); // ❗ Ensure correct DTO name

                // Repository call
                List<Asset> assets = await _unitOfWork.AssetRepository.GetAllAssetAsync(filterAsset);

                // Map result to DTO
                List<AssetResponseDTO> getAllAssetDTOs = _mapper.Map<List<AssetResponseDTO>>(assets);

                _logger.LogInformation("Successfully retrieved {AssetCount} asset(s).", getAllAssetDTOs.Count);

                return new ApiResponse<List<AssetResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "Assets fetched successfully.",
                    Data = getAllAssetDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching assets.");
                return new ApiResponse<List<AssetResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "Error occurred while fetching assets.",
                    Data = null
                };
            }
        }
    }
}
