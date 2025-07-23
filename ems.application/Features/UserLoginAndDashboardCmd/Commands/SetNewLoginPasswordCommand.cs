using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginAndDashboardCmd.Commands
{
    public class SetNewLoginPasswordCommand :IRequest<ApiResponse<UpdateLoginPasswordResponseDTO>>
    {
        //till completed
     public NewLoginPasswordRequestDTO dto { get; set; }
    public SetNewLoginPasswordCommand(NewLoginPasswordRequestDTO dto)
    {
        this.dto = dto;
    }


}
}
