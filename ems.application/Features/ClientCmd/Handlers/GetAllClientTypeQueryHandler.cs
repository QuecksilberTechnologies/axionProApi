using AutoMapper;
using ems.application.Features.ClientCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Features.ClientCmd.Queries;

using ems.application.Features.LeaveCmd.Handlers;
using Microsoft.Extensions.Logging;
using ems.application.DTOs.Client;

namespace ems.application.Features.ClientCmd.Handlers
{
    internal class GetAllClientTypeQueryHandler : IRequestHandler<GetAllClientTypeQuery, ApiResponse<List<GetAllClientTypeDTO>>>
    {
       // private readonly IClientRepository _clienttypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllLeaveTypeQueryHandler> _logger;



        public GetAllClientTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllLeaveTypeQueryHandler> logger)      
        {
           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<ApiResponse<List<GetAllClientTypeDTO>>> Handle(GetAllClientTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Correcting the method call
                List<ClientType> clientTypes = await _unitOfWork.ClientsRepository.GetAllClientTypeAsync();

                //if (clientTypes == null || !clientTypes.Any())
                //{
                //    _logger.LogWarning("No clientTypes found.");
                //    return new ApiResponse<List<GetAllClientTypeDTO>>(false, "No clientTypes found", new List<GetAllClientTypeQuery>());
                //}

                //// ✅ Map Role entities to DTOs
                var getAllClientTypeDTOs = _mapper.Map<List<GetAllClientTypeDTO>>(clientTypes);

                _logger.LogInformation("Successfully retrieved {Count} roles.", getAllClientTypeDTOs.Count);
                return new ApiResponse<List<GetAllClientTypeDTO>>
                {
                    IsSuccecced = true,
                    Message = "Categories fetched successfully.",
                    Data = getAllClientTypeDTOs
                };
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching roles.");
                return new ApiResponse<List<GetAllClientTypeDTO>>
                {
                    IsSuccecced = false,
                    Message = "Categories fetched successfully.",
                    Data = null
                };
            }

        }
    }
}