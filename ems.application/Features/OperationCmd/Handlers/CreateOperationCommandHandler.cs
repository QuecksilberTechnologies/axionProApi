using AutoMapper;
 
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
using ems.application.Features.OperationCmd.Commands;
using ems.application.DTOs.Operation;

namespace ems.application.Features.OperationCmd.Handlers
{
    public class CreateOperationCommandHandler :  IRequestHandler<CreateOperationCommand, ApiResponse<List<GetAllOperationDTO>>>
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOperationCommandHandler(IOperationRepository operationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _operationRepository = operationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<List<GetAllOperationDTO>>> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Operation operationEntity = _mapper.Map<Operation>(request.createOperationDTO);
                List<Operation> operations = await _operationRepository.CreateOperationAsync(operationEntity);

                if (operations == null || !operations.Any())
                {
                    return new ApiResponse<List<GetAllOperationDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No Operation were created.",
                        Data = new List<GetAllOperationDTO>()
                    };
                }

                List<GetAllOperationDTO> operationDTOs = _mapper.Map<List<GetAllOperationDTO>>(operations);

                return new ApiResponse<List<GetAllOperationDTO>>
                {
                    IsSucceeded = true,
                    Message = " Operation created successfully",
                    Data = operationDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while creating role.");
                return new ApiResponse<List<GetAllOperationDTO>>

                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }

}
