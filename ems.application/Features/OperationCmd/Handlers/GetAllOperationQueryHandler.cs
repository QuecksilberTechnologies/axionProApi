using AutoMapper;
using ems.application.DTOs.Operation;
 
using ems.application.Features.OperationCmd.Queries;
using ems.application.Features.TransportCmd.Handlers;
using ems.application.Features.TransportCmd.Queries;
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

namespace ems.application.Features.OperationCmd.Handlers
{
    public class GetAllOperationQueryHandler : IRequestHandler<GetAllOperationQuery, ApiResponse<List<GetAllOperationDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllOperationQueryHandler> _logger;

        public GetAllOperationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllOperationQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<List<GetAllOperationDTO>>> Handle(GetAllOperationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Correcting the method call

                List<Operation> operationDTOs = await _unitOfWork.OperationRepository.GetAllOperationAsync();

                //if (roles == null || !roles.Any())
                //{
                //    _logger.LogWarning("No Operations found.");
                //    return new ApiResponse<List<GetAllRoleDTO>>(false, "No Operations found", new List<GetAllRoleDTO>());
                //}

                //// ✅ Map Role entities to DTOs
                var getAllOperationDTOs = _mapper.Map<List<GetAllOperationDTO>>(operationDTOs);

                _logger.LogInformation("Successfully retrieved {Count} Operations.", getAllOperationDTOs.Count);
                return new ApiResponse<List<GetAllOperationDTO>>
                {
                    IsSuccecced = true,
                    Message = "Operations fetched successfully.",
                    Data = getAllOperationDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Operations.");
                return new ApiResponse<List<GetAllOperationDTO>>
                {
                    IsSuccecced = false,
                    Message = "Operations fetched successfully.",
                    Data = null
                };
            }
        }



    }
}
