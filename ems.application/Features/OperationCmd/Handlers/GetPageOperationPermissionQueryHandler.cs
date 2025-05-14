using AutoMapper;
using ems.application.DTOs.Operation;
using ems.application.Features.OperationCmd.Queries;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.OperationCmd.Handlers
{
    public class GetPageOperationPermissionQueryHandler : IRequestHandler<GetPageOperationPermissionQuery, ApiResponse<HasAccessOperationDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPageOperationPermissionQueryHandler> _logger;

        public GetPageOperationPermissionQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetPageOperationPermissionQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<HasAccessOperationDTO>> Handle(GetPageOperationPermissionQuery request, CancellationToken cancellationToken)
        {

            var response = new ApiResponse<HasAccessOperationDTO>();

            try
            {
                var requestDTO = request.CheckOperationPermissionRequest;


                // Repository se permission check karo
                bool result = await _unitOfWork.CommonRepository.GetHasAccessOperation(requestDTO);
                if (result)
                {
                    response.IsSuccecced = result;

                    response.Data = new HasAccessOperationDTO
                    {
                        Status = result, // Assign the bool directly
                        Message = "✅ Permission checked successfully.",
                        Success = result

                    };
                }
                else
                {
                    response.IsSuccecced = result;
                    response.Data = new HasAccessOperationDTO
                    {
                        Status = result, // Assign the bool directly
                        Message = "✅ Not have permission.",
                        Success = result

                    };
                }

              
            }
            catch (Exception ex)
            {
                response.IsSuccecced = false;
                response.Message = "❌ An error occurred while checking permission.";
                _logger.LogError($"🚨 Error in GetPageOperationPermissionQueryHandler: {ex.Message}");
            }

            return response;
        }
    }
}
