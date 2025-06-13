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
    public class DeleteStatusByTenantCommandHandler : IRequestHandler<DeleteStatusByTenantCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteStatusByTenantCommandHandler> _logger;

        public DeleteStatusByTenantCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<DeleteStatusByTenantCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteStatusByTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO ko Entity mein map karein


                AssetStatus assetStatus = _mapper.Map<AssetStatus>(request.deleteAssetStatusRequest);              

                bool isDeleted = await _unitOfWork.AssetRepository.DeleteAssetStatusByTenantAsync(assetStatus);
                // Entity list to DTO list
             
                return new ApiResponse<bool>
                {
                    IsSucceeded = true,
                    Message = "Asset Status successfully deleted .",
                    Data = isDeleted
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding asset status.");
                return  new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    Message = "Something went wrong while adding asset status.",
                    Data = false
                };
            }
        }
    }

}