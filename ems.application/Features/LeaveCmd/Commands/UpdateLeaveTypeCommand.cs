using ems.application.DTOs.Leave;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.LeaveCmd.Commands
{
    public class UpdateLeaveTypeCommand : IRequest<ApiResponse<List<GetAllLeaveTypeDTO>>>
    {
        
            public UpdateLeaveTypeDTO UpdateLeaveTypeDTO { get; set; }

    public UpdateLeaveTypeCommand(UpdateLeaveTypeDTO updateLeaveTypeDTO)
    {
        this.UpdateLeaveTypeDTO = updateLeaveTypeDTO;
    }

}
     
}