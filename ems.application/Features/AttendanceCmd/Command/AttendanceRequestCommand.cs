using ems.application.DTOs.AttendanceDTO;
using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.AttendanceCmd.Command
{
    public class AttendanceCommand : IRequest<ApiResponse<AttendanceResponseDTO>>
    {
        public AttendanceRequestDTO AttendanceRequestDTO { get; set; }


        public AttendanceCommand(AttendanceRequestDTO attendanceRequestDTO)
        {
            AttendanceRequestDTO = attendanceRequestDTO;
        }



    }
}
