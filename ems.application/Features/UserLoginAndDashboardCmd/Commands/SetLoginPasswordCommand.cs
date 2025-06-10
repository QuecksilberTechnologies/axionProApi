using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using ems.domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginAndDashboardCmd.Commands
{
       public class SetLoginPasswordCommand : IRequest<ApiResponse<SetLoginPasswordResponseDTO>>
    {
        public LoginRequestDTO? setLoginPasswordRequest { get; set; }


        public SetLoginPasswordCommand(LoginRequestDTO? setLoginPasswordRequest)
        {
            this.setLoginPasswordRequest = setLoginPasswordRequest;
        }



    }
}
