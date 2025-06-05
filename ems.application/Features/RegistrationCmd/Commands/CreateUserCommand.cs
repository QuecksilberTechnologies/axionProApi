using ems.application.DTOs.Registration;
using ems.application.DTOs.UserLogin;
using ems.application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Features.RegistrationCmd.Commands
{
    public class CreateUserCommand  : IRequest<ApiResponse<LoginResponseDTO>>
    {
        public LoginRequestDTO LoginRequestDTO { get; set; }

        public CreateUserCommand(LoginRequestDTO loginRequestDTO)
        {
            LoginRequestDTO = loginRequestDTO;
        }

    }
    
}
