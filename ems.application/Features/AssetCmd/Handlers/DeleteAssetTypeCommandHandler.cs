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
    public class DeleteAssetTypeCommandHandler : IRequestHandler<DeleteAssetTypeCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAssetTypeCommand> _logger;

        public DeleteAssetTypeCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<DeleteAssetTypeCommand> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteAssetTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO ko Entity mein map karein


                AssetType assetType = _mapper.Map<AssetType>(request.deleteAssetTypeRequestDTO);

                bool isDeleted = await _unitOfWork.AssetRepository.DeleteAssetTypeByTenantAsync(assetType);
                // Entity list to DTO list

                return new ApiResponse<bool>
                {
                    IsSucceeded = true,
                    Message = "Asset Type successfully deleted .",
                    Data = isDeleted
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding asset type.");
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    Message = "Something went wrong while adding asset type.",
                    Data = false
                };
            }
        }
    }
}
 