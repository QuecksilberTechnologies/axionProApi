using AutoMapper;
using ems.application.DTOs.Leave;
using ems.application.Features.LeaveCmd.Commands;
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
using ems.application.DTOs.Module;
using ems.application.Features.ModuleCmd.Commands;
using Microsoft.Extensions.Logging;

namespace ems.application.Features.ModuleCmd.Handlers
{
    public class CreateSubModuleCommandHandler : IRequestHandler<CreateSubModuleCommand, ApiResponse<ModuleResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSubModuleCommandHandler> _logger;

        public CreateSubModuleCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<CreateSubModuleCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<ModuleResponseDTO>> Handle(CreateSubModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ DTO to Entity mapping
                var moduleEntity = _mapper.Map<Module>(request);

                // ✅ Set default values
                moduleEntity.IsActive = true;
                moduleEntity.AddedDateTime = DateTime.Now;

                // ✅ Insert into DB
                var addedModule = await _unitOfWork.ModuleRepository.AddSubModuleAsync(moduleEntity);

                // ✅ Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                // ✅ Map response DTO
                var responseDTO = _mapper.Map<ModuleResponseDTO>(addedModule);

                _logger.LogInformation("Module created successfully with ID {ModuleId}", addedModule.Id);

                return new ApiResponse<ModuleResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Module created successfully.",
                    Data = responseDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating module.");
                return new ApiResponse<ModuleResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "Failed to create module.",
                    Data = null
                };
            }
        }
    }
}
