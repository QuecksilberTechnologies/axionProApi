using AutoMapper;
 
using ems.application.Features.RoleCmd.Commands;
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
using ems.application.Features.LeaveCmd.Commands;
using ems.application.DTOs.Leave;

namespace ems.application.Features.LeaveCmd.Handlers
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, ApiResponse<List<GetAllLeaveTypeDTO>>>
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonRepository _commonRepository;

        public CreateLeaveTypeCommandHandler( ILeaveRepository leaveRepository,  IMapper mapper, IUnitOfWork unitOfWork, ICommonRepository commonRepository) // Inject CommonRepository
        {
            _leaveRepository = leaveRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _commonRepository = commonRepository;
        }
       
        public async Task<ApiResponse<List<GetAllLeaveTypeDTO>>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //ID hai Id se UserRollTable se roll fetch karna hooga par jiska isprimartyrole true hoo, 
                // ab wo roleid se 
                // ✅ Step 1: User ID se Primary Role Fetch Karo
                string Password = "123";
                var primaryRoleId = await _commonRepository.ValidateUserLoginAsync(request.createLeaveTypeDTO.AddedById.ToString() );

                if (primaryRoleId !=1)
                {
                    return new ApiResponse<List<GetAllLeaveTypeDTO>>
                    {
                        IsSuccecced = false,
                        Message = "No primary role found for the user.",
                        Data = null
                    };
                }


                LeaveType leavetypeEntity = _mapper.Map<LeaveType>(request.createLeaveTypeDTO);
                List<LeaveType> leaveTypes = await _leaveRepository.CreateLeaveAsync(leavetypeEntity);

                if (leaveTypes == null || !leaveTypes.Any())
                {
                    return new ApiResponse<List<GetAllLeaveTypeDTO>>
                    {
                        IsSuccecced = false,
                        Message = "No Leave were created.",
                        Data = new List<GetAllLeaveTypeDTO>()
                    };
                }

                List<GetAllLeaveTypeDTO> leaveTypeDTOs = _mapper.Map<List<GetAllLeaveTypeDTO>>(leaveTypes);

                return new ApiResponse<List<GetAllLeaveTypeDTO>>
                {
                    IsSuccecced = true,
                    Message = "Leave created successfully",
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
