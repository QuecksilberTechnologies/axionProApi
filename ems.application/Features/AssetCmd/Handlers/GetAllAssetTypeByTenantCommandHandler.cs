using AutoMapper;
using ems.application.Constants;
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
    public class GetAllAssetTypeByTenantCommandHandler : IRequestHandler<GetAllAssetTypeByTenantCommand, ApiResponse<List<AssetTypeResponseDTO>>>

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllAssetTypeByTenantCommandHandler> _logger;

        public GetAllAssetTypeByTenantCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetAllAssetTypeByTenantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<AssetTypeResponseDTO>>> Handle(GetAllAssetTypeByTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Step 1: Map request DTO to AssetType entity
                  AssetType asset = _mapper.Map<AssetType>(request.AssetTypeRequest);               

                // Step 2: Add asset type and get updated list
                List<AssetType> assetTypeList = await _unitOfWork.AssetRepository.GetAllAssetTypeByTenantAsync(request.AssetTypeRequest);

                // Step 3: Map entity list to response DTO

                List<AssetTypeResponseDTO> responseDTOs = _mapper.Map<List<AssetTypeResponseDTO>>(assetTypeList);


                return ApiResponse<List<AssetTypeResponseDTO>>.Success(responseDTOs, "Asset Type response.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding asset type.");
                return ApiResponse<List<AssetTypeResponseDTO>>.Fail("Error");
            }
        }

    }

}