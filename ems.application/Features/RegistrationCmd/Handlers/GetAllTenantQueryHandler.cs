using AutoMapper;
using ems.application.DTOs.Operation;
using ems.application.DTOs.Region;
using ems.application.DTOs.Registration;
using ems.application.DTOs.Tenant;
using ems.application.Features.RegistrationCmd.Commands;
using ems.application.Features.RegistrationCmd.Queries;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
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
 
        public class GetAllTenantQueryHandler : IRequestHandler<GetAllTenantQuery, ApiResponse<List<GetAllTenantDTO>>>
         {
            private readonly ITenantRepository _tenantRepository;
            private readonly IMapper _mapper;
           private readonly IUnitOfWork _unitOfWork;
           private readonly ILogger<GetAllTenantQueryHandler> _logger;

            public GetAllTenantQueryHandler(
            ITenantRepository tenantRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetAllTenantQueryHandler> logger)
            {
                _tenantRepository = tenantRepository;
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }
  
        public async Task<ApiResponse<List<GetAllTenantDTO>>> Handle(GetAllTenantQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Correcting the method call

                List<Tenant> tenants = await _unitOfWork.TenantRepository.GetAllTenantAsync();

                var getAllTenantsDTOs = _mapper.Map<List<GetAllTenantDTO>>(tenants);

                _logger.LogInformation("Successfully retrieved {Count} Tenants.", getAllTenantsDTOs.Count);
                return new ApiResponse<List<GetAllTenantDTO>>
                {
                    IsSucceeded = true,
                    Message = "Tenants fetched successfully.",
                    Data = getAllTenantsDTOs
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Tenants.");
                return new ApiResponse<List<GetAllTenantDTO>>
                {
                    IsSucceeded = false,
                    Message = "Tenants not fetched .",
                    Data = null
                };
            }
        }

         
    }
}
