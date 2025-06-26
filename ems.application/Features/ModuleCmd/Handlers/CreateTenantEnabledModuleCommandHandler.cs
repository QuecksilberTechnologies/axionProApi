using AutoMapper;
using ems.application.DTOs.Module;
using ems.application.Features.ModuleCmd.Commands;
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

namespace ems.application.Features.ModuleCmd.Handlers
{
    public class CreateTenantEnabledModuleCommandHandler : IRequestHandler<CreateTenantEnabledModuleCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTenantEnabledModuleCommandHandler> _logger;

        public CreateTenantEnabledModuleCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<CreateTenantEnabledModuleCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(CreateTenantEnabledModuleCommand request, CancellationToken cancellationToken)
        {
           

            

         //   await _tenantModuleRepository.SaveEnabledModulesAsync(moduleEntities, operationEntities);

          //  return new ApiResponse<bool>(true, "Tenant enabled modules & operations saved successfully.");
            return null;

        }
    }
}
