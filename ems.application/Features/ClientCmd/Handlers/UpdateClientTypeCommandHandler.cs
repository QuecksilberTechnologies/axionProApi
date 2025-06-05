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
using ems.application.DTOs.Client;

namespace ems.application.Features.ClientCmd.Handlers
{
    public class UpdateClientTypeCommandHandler : IRequestHandler<UpdateClientTypeCommand, ApiResponse<List<GetAllClientTypeDTO>>>
    {
        private readonly IClientRepository _ClientRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientTypeCommandHandler(IClientRepository ClientRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _ClientRepository = ClientRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<List<GetAllClientTypeDTO>>> Handle(UpdateClientTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ClientType ClienttypeEntity = _mapper.Map<ClientType>(request.updateClientTypeCommand);
                List<ClientType> ClientTypes = await _ClientRepository.UpdateClientTypeAsync(ClienttypeEntity);

                if (ClientTypes == null || !ClientTypes.Any())
                {
                    return new ApiResponse<List<GetAllClientTypeDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No Client were update.",
                        Data = new List<GetAllClientTypeDTO>()
                    };
                }

                List<GetAllClientTypeDTO> ClientTypeDTOs = _mapper.Map<List<GetAllClientTypeDTO>>(ClientTypes);

                return new ApiResponse<List<GetAllClientTypeDTO>>
                {
                    IsSucceeded = true,
                    Message = "Client update successfully",
                    Data = ClientTypeDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while creating role.");
                return new ApiResponse<List<GetAllClientTypeDTO>>

                {
                    IsSucceeded = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }


}