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
        public string LoginId { get; set; }
        public string Password { get; set; }

        public LoginCommand(string loginId, string password)
        {
            LoginId = loginId;
            Password = password;
        }
    }

}
