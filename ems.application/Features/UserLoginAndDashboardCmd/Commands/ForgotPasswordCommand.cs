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
    public class ForgotPasswordCommand : IRequest<ApiResponse<ForgotPasswordResponseDTO>>
    {
        public ForgotPasswordUserIdRequestDTO dTO { get; set; }


        public ForgotPasswordCommand(ForgotPasswordUserIdRequestDTO dto)
        {
            dTO = dto;
        }



    }
}