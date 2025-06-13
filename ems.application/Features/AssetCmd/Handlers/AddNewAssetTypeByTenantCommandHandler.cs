using AutoMapper;
using ems.application.Constants;
using ems.application.DTOs.Asset;
using ems.application.DTOs.UserLogin;
using ems.application.Features.AssetCmd.Commands;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.AssetCmd.Handlers
{
    public class AddNewAssetTypeByTenantCommandHandler: IRequestHandler<AddNewAssetTypeByTenantCommand, ApiResponse<AssetTypeResponseDTO>>

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddNewAssetTypeByTenantCommandHandler> _logger;

        public AddNewAssetTypeByTenantCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<AddNewAssetTypeByTenantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<AssetTypeResponseDTO>> Handle(AddNewAssetTypeByTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Step 1: Map request DTO to AssetType entity
                AssetType asset = _mapper.Map<AssetType>(request.assetStatusRequestDTO);

                asset.AddedById = request.assetStatusRequestDTO.EmployeeId;

                // Step 2: Add asset type and get updated list
                AssetType assetTypeList = await _unitOfWork.AssetRepository.AddAssetTypeAsync(asset);

                // Step 3: Map entity list to response DTO
           
                AssetTypeResponseDTO responseDTOs = _mapper.Map<AssetTypeResponseDTO>(assetTypeList);


                return ApiResponse<AssetTypeResponseDTO>.Success(responseDTOs, "Asset Type added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding asset type.");
                return ApiResponse<AssetTypeResponseDTO>.Fail(ConstantValues.Duplicate);
            }
        }

    }

}