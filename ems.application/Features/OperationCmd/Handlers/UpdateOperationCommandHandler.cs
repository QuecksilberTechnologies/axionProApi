using AutoMapper;

using ems.application.Features.TransportCmd.Commands;
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
using ems.application.Features.OperationCmd.Commands;
using ems.application.DTOs.Operation;

namespace ems.application.Features.OperationCmd.Handlers
{
    public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, ApiResponse<List<GetAllOperationDTO>>>
    {
        private readonly IOperationRepository operationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOperationCommandHandler(IOperationRepository operationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.operationRepository = operationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<GetAllOperationDTO>>> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
        {

            try
            {

                Operation operation = _mapper.Map<Operation>(request.updateOperationDTO);
                List<Operation> operations = await operationRepository.UpdateOperationAsync(operation);

                if (operations == null || !operations.Any())
                {
                    return new ApiResponse<List<GetAllOperationDTO>>
                    {
                        IsSuccecced = false,
                        Message = "No operation were updated.",
                        Data = new List<GetAllOperationDTO>()
                    };
                }

                List<GetAllOperationDTO> getAllOperationDTOs = _mapper.Map<List<GetAllOperationDTO>>(operations);

                return new ApiResponse<List<GetAllOperationDTO>>
                {
                    IsSuccecced = true,
                    Message = "Travel created successfully",
                    Data = getAllOperationDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while Updatiing Operation.");
                return new ApiResponse<List<GetAllOperationDTO>>

                {
                    IsSuccecced = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }
}