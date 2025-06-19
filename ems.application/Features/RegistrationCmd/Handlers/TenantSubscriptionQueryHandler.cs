using AutoMapper;
using ems.application.DTOs.Tenant;
using ems.application.Features.RegistrationCmd.Queries;
using ems.application.Interfaces.IRepositories;
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

namespace ems.application.Features.RegistrationCmd.Handlers
{
    public class TenantSubscriptionQueryHandler : IRequestHandler<TenantSubscriptionQuery, ApiResponse<TenantSubscriptionPlanResponseDTO>>
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TenantSubscriptionQueryHandler> _logger;
        private ITenantSubscriptionRepository? _tenantSubscriptionRepository;
        public TenantSubscriptionQueryHandler(ITenantSubscriptionRepository? _tenantSubscriptionRepository,


        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<TenantSubscriptionQueryHandler> logger)
        {
           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            this._tenantSubscriptionRepository = _tenantSubscriptionRepository;
        }
 
    public async Task<ApiResponse<TenantSubscriptionPlanResponseDTO>> Handle(TenantSubscriptionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Map DTO to entity
            var tenantSubscriptionEntity = _mapper.Map<TenantSubscription>(request.tenantSubscriptionPlanRequest);
            // Fetch from repository (you must implement this method)
            TenantSubscription? tenantSubscriptionPlan = await _unitOfWork.TenantSubscriptionRepository.GetTenantSubscriptionAsync(tenantSubscriptionEntity);

            // Map to response DTO
            var responseDTO = _mapper.Map<TenantSubscriptionPlanResponseDTO>(tenantSubscriptionPlan);

            if (responseDTO == null)
            {
                _logger.LogWarning("No tenant subscription plan found.");
                return new ApiResponse<TenantSubscriptionPlanResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "No tenant subscription plan found.",
                    Data = new TenantSubscriptionPlanResponseDTO()
                };
            }

            _logger.LogInformation("Successfully retrieved tenant subscription for TenantId: {TenantId}", responseDTO.TenantId);

            return new ApiResponse<TenantSubscriptionPlanResponseDTO>
            {
                IsSucceeded = true,
                Message = "Tenant subscription plan fetched successfully.",
                Data = responseDTO
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching tenant subscription plan.");
            return new ApiResponse<TenantSubscriptionPlanResponseDTO>
            {
                IsSucceeded = false,
                Message = "Error occurred while fetching tenant subscription plan.",
                Data = null
            };
        }
    }
   
     
     }


    
}
