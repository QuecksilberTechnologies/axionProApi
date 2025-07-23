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
    public class CreateLeaveTypeCommand: IRequest<ApiResponse<List<GetAllLeaveTypeDTO>>>
    {
        
            public CreateLeaveTypeDTO createLeaveTypeDTO { get; set; }

            public CreateLeaveTypeCommand(CreateLeaveTypeDTO createLeaveTypeDTO)
            {
                this.createLeaveTypeDTO = createLeaveTypeDTO;
            }

        }
     
}
