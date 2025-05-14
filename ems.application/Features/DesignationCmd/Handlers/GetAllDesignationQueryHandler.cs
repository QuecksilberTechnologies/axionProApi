using AutoMapper;
using ems.application.DTOs.Designation;
 
using ems.application.Features.DesignationCmd.Queries;
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

namespace ems.application.Features.DesignationCmd.Handlers
{
    public class GetAllDesignationQueryHandler: IRequestHandler<GetAllDesignationQuery, ApiResponse<List<GetAllDesignationDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllDesignationQueryHandler> _logger;

        public GetAllDesignationQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllDesignationQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetAllDesignationDTO>>> Handle(GetAllDesignationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Correcting the method call

                List<Designation> designations = await _unitOfWork.DesignationRepository.GetAllDesignationAsync();

                //if (designations == null || !designations.Any())
                //{
                //    _logger.LogWarning("No designations found.");
                //    return new ApiResponse<List<GetAllRoleDTO>>(false, "No Designation found", new List<GetAllRoleDTO>());
                //}

                /// Map designations entities to DTOs
                List<GetAllDesignationDTO>  getAllOperationDTOs = _mapper.Map<List<GetAllDesignationDTO>>(designations);

                _logger.LogInformation("Successfully retrieved {Count} Designation.", getAllOperationDTOs.Count);
                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSuccecced = true,
                    Message = "Operations fetched successfully.",
                    Data = getAllOperationDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Designation.");
                return new ApiResponse<List<GetAllDesignationDTO>>
                {
                    IsSuccecced = false,
                    Message = "Designation fetched successfully.",
                    Data = null
                };
            }
        }

       
    }
}
