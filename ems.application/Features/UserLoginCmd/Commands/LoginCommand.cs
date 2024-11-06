using ems.application.DTOs.EmployeeDTO;
using ems.application.DTOs.UserLogin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.UserLoginCmd.Commands
{
    public class LoginCommand : IRequest<LoginResponseDTO>
    {
        public LoginRequestDTO RequestLoginDTO { get; set; }

        public LoginCommand(LoginRequestDTO loginRequestDTO )
        {
            RequestLoginDTO= loginRequestDTO;
        }
    }

}
