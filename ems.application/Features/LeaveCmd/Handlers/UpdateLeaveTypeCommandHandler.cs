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

using ems.application.DTOs.Leave;

namespace ems.application.Features.LeaveCmd.Handlers
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, ApiResponse<List<GetAllLeaveTypeDTO>>>
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeaveTypeCommandHandler(ILeaveRepository leaveRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _leaveRepository = leaveRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<List<GetAllLeaveTypeDTO>>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                LeaveType leavetypeEntity = _mapper.Map<LeaveType>(request.UpdateLeaveTypeDTO);
                List<LeaveType> leaveTypes = await _leaveRepository.UpdateLeaveAsync(leavetypeEntity);

                if (leaveTypes == null || !leaveTypes.Any())
                {
                    return new ApiResponse<List<GetAllLeaveTypeDTO>>
                    {
                        IsSuccecced = false,
                        Message = "No Leave were update.",
                        Data = new List<GetAllLeaveTypeDTO>()
                    };
                }
                
                List<GetAllLeaveTypeDTO> leaveTypeDTOs = _mapper.Map<List<GetAllLeaveTypeDTO>>(leaveTypes);

                return new ApiResponse<List<GetAllLeaveTypeDTO>>
                {
                    IsSuccecced = true,
                    Message = "Leave update successfully",
                    Data = leaveTypeDTOs
                };
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex, "Error occurred while creating role.");
                return new ApiResponse<List<GetAllLeaveTypeDTO>>

                {
                    IsSuccecced = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }


}