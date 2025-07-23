using AutoMapper;
 
using ems.application.Features.LeaveCmd.Commands;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces;
using ems.application.Wrappers;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ems.application.Features.ClientCmd.Commands;
using ems.application.DTOs.Client;

namespace ems.application.Features.ClientCmd.Handlers
{
    public class CreateClientTypeCommandHandler :IRequestHandler<CreateClientTypeCommand, ApiResponse<List<GetAllClientTypeDTO>>>
    {
    private readonly IClientRepository _clienttypeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientTypeCommandHandler(IClientRepository clientRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
            _clienttypeRepository = clientRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    

        public  async Task<ApiResponse<List<GetAllClientTypeDTO>>> Handle(CreateClientTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ClientType clientType = _mapper.Map<ClientType>(request.createClientTypeDTO);
                List<ClientType> clientTypes = await _clienttypeRepository.CreateClientTypeAsync(clientType);

                if (clientTypes == null || !clientTypes.Any())
                {
                    return new ApiResponse<List<GetAllClientTypeDTO>>
                    {
                        IsSucceeded = false,
                        Message = "No Leave were created.",
                        Data = new List<GetAllClientTypeDTO>()
                    };
                }

                List<GetAllClientTypeDTO> leaveTypeDTOs = _mapper.Map<List<GetAllClientTypeDTO>>(clientTypes);

                return new ApiResponse<List<GetAllClientTypeDTO>>
                {
                    IsSucceeded = true,
                    Message = "Leave created successfully",
                    Data = leaveTypeDTOs
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
 