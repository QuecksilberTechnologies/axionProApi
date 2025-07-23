using AutoMapper;
using ems.application.Features.SubscriptionCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.application.Features.DesignationCmd.Commands;
using ems.application.DTOs.RoleModulePermission;
using ems.application.DTOs.SubscriptionModule;

namespace ems.application.Features.SubscriptionCmd.Handlers
{
    public class GetActiveTenantSubscriptionPlanIdByTenantIdCommandHandler : IRequestHandler<GetActiveTenantSubscriptionPlanIdByTenantIdCommand, ApiResponse<GetTenantSubscriptionDetailResponsDTO>>
    {
        private readonly ITenantSubscriptionRepository _tenantSubscriptionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActiveTenantSubscriptionPlanIdByTenantIdCommandHandler> _logger;


        public GetActiveTenantSubscriptionPlanIdByTenantIdCommandHandler(ITenantSubscriptionRepository tenantSubscriptionRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetActiveTenantSubscriptionPlanIdByTenantIdCommandHandler> logger)
        {
            _tenantSubscriptionRepository = tenantSubscriptionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<GetTenantSubscriptionDetailResponsDTO>> Handle(GetActiveTenantSubscriptionPlanIdByTenantIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Validation
                if (request == null)
                {
                    _logger.LogWarning("GetPlanModuleMappingCommand is null.");
                    return new ApiResponse<GetTenantSubscriptionDetailResponsDTO>
                    {
                        IsSucceeded = false,
                        Message = "Request cannot be null.",
                        Data = null
                    };
                }

                // ✅ Get all plans
                var subscriptionPlans = await _unitOfWork.TenantSubscriptionRepository
                 .GetTenantActiveSubscriptionPlanDetail(request.dto);

                if (subscriptionPlans == null)
                {
                    return new ApiResponse<GetTenantSubscriptionDetailResponsDTO>
                    {
                        IsSucceeded = false,
                        Message = "No active subscription plan found for the tenant.",
                        Data = null
                    };
                }

                return new ApiResponse<GetTenantSubscriptionDetailResponsDTO>
                {
                    IsSucceeded = true,
                    Message = "Subscription plan fetched successfully.",
                    Data = subscriptionPlans
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching plans.");
                return new ApiResponse<GetTenantSubscriptionDetailResponsDTO>
                {
                    IsSucceeded = false,
                    Message = "Something went wrong while fetching plans.",
                    Data = null
                };
            }
        }
    }
}
