using AutoMapper;
using ems.application.DTOs.Module;
using ems.application.DTOs.Module.NewFolder;
using ems.application.Features.ModuleCmd.Commands;
using ems.application.Interfaces;
using ems.application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Handlers
{
    public class GetModulesDDLCommandHandler : IRequestHandler<GetModulesDLLCommand, ApiResponse<List<GetModuleDDLResponseDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetModulesDDLCommandHandler> _logger;

        public GetModulesDDLCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<GetModulesDDLCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<List<GetModuleDDLResponseDTO>>> Handle(GetModulesDLLCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modules = await _unitOfWork.ModuleRepository.GetAllModulesDDLAsync(false);
                var mappedModules = _mapper.Map<List<GetModuleDDLResponseDTO>>(modules);

                _logger.LogInformation("Fetched {Count} modules for DDL.", mappedModules.Count);

                return new ApiResponse<List<GetModuleDDLResponseDTO>>
                {
                    IsSucceeded = true,
                    Message = "Modules fetched successfully.",
                    Data = mappedModules
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching module DDL.");
                return new ApiResponse<List<GetModuleDDLResponseDTO>>
                {
                    IsSucceeded = false,
                    Message = "Failed to fetch modules.",
                    Data = null
                };
            }
        }
    }
}
