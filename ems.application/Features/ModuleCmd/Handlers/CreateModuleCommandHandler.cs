using AutoMapper;
using ems.application.DTOs.Module;
using ems.application.DTOs.Module.NewFolder;
using ems.application.Features.ModuleCmd.Commands;
using ems.application.Interfaces;
using ems.application.Interfaces.IRepositories;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ems.application.Features.ModuleCmd.Handlers
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, ApiResponse<MainModuleResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateModuleCommandHandler> _logger;

        public CreateModuleCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<CreateModuleCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<MainModuleResponseDTO>> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ DTO to Entity mapping
                var moduleEntity = _mapper.Map<Module>(request.DTO);

                // ✅ Set default values

                moduleEntity.AddedById = request.DTO.ProductOwnerId;               
                moduleEntity.ParentModuleId = null;
                moduleEntity.AddedDateTime = DateTime.Now;

                // ✅ Insert into DB
                var addedModule = await _unitOfWork.ModuleRepository.AddModuleAsync(moduleEntity);


                // ✅ Commit transaction
                await _unitOfWork.CommitTransactionAsync();

                // ✅ Map response DTO
                
                _logger.LogInformation("Module created successfully with ID {ModuleId}", addedModule.Id);
                var responseDTO = _mapper.Map<MainModuleResponseDTO>(addedModule);

                return new ApiResponse<MainModuleResponseDTO>
                {
                    IsSucceeded = true,
                    Message = "Module created successfully.",
                    Data = responseDTO
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating module.");
                return new ApiResponse<MainModuleResponseDTO>
                {
                    IsSucceeded = false,
                    Message = "Failed to create module.",
                    Data = null
                };
            }
        }
    }
}
