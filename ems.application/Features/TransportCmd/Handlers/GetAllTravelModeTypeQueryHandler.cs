using AutoMapper;
 
using ems.application.DTOs.Transport;
using ems.application.Features.LeaveCmd.Handlers;
using ems.application.Features.LeaveCmd.Queries;
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

namespace ems.application.Features.TransportCmd.Handlers
{
    public class GetAllTravelModeTypeQueryHandler : IRequestHandler<GetAllTravelModeTypeQuery, ApiResponse<List<GetAllTravelModeDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllTravelModeTypeQueryHandler> _logger;

        public GetAllTravelModeTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllTravelModeTypeQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiResponse<List<GetAllTravelModeDTO>>> Handle(GetAllTravelModeTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Correcting the method call
                List<TravelMode> travelModes = await _unitOfWork.TravelRepository.GetAllTravelModeTypeAsync();

                //if (roles == null || !roles.Any())
                //{
                //    _logger.LogWarning("No roles found.");
                //    return new ApiResponse<List<GetAllRoleDTO>>(false, "No roles found", new List<GetAllRoleDTO>());
                //}

                //// ✅ Map Role entities to DTOs
                var travelDTOs = _mapper.Map<List<GetAllTravelModeDTO>>(travelModes);

                _logger.LogInformation("Successfully retrieved {Count} travelModes.", travelDTOs.Count);
                return new ApiResponse<List<GetAllTravelModeDTO>>
                {
                    IsSucceeded = true,
                    Message = "travelModes fetched successfully.",
                    Data = travelDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching roles.");
                return new ApiResponse<List<GetAllTravelModeDTO>>
                {
                    IsSucceeded = false,
                    Message = "Categories fetched successfully.",
                    Data = null
                };
            }
        }
         

       
    }
}
