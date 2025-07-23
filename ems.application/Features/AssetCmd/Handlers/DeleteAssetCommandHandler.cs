using AutoMapper;
using ems.application.Constants;
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
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAssetCommand> _logger;

        public DeleteAssetCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<DeleteAssetCommand> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // DTO ko Entity mein map karein


                Asset asset = _mapper.Map<Asset>(request.deleteAssetDTO);

                int ret = await _unitOfWork.AssetRepository.DeleteAssetAsync(asset);
                // Entity list to DTO list
                if(ret == 0) 

                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    Message = "Getting null issue.",
                    Data = ConstantValues.IsByDefaultFalse

                };
                if(ret == -1)
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    Message = "Asset is assigned.",
                    Data = ConstantValues.IsByDefaultFalse

                };
                if (ret == -2)
                    return new ApiResponse<bool>
                    {
                        IsSucceeded = false,
                        Message = "Asset is assigned.",
                        Data = ConstantValues.IsByDefaultFalse

                    };
                
                    return new ApiResponse<bool>
                    {
                        IsSucceeded = true,
                        Message = "Asset is deleted.",
                        Data = ConstantValues.IsByDefaultTrue

                    };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft deleting Asset..");
                return new ApiResponse<bool>
                {
                    IsSucceeded = false,
                    Message = "Error occurred while soft deleting Asset.t .",
                    Data = false
                };
            }
        }
    }
}
