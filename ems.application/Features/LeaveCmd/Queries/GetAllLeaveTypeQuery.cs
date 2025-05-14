using ems.application.DTOs.Leave;
 
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.LeaveCmd.Queries
{
    public class GetAllLeaveTypeQuery : IRequest<ApiResponse<List<GetAllLeaveTypeDTO>>>
    {
        public LeaveTypeRequestDTO GetAllLeaveTypeDTO { get; set; }

        public GetAllLeaveTypeQuery(LeaveTypeRequestDTO leaveTypeRequestDTO)
        {
            this.GetAllLeaveTypeDTO = leaveTypeRequestDTO;
        }
    }
}
 
