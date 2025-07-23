using ems.application.DTOs.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IUserLoginReopsitory
    {
        Task<LoginResponseDTO> AuthenticateUser(LoginRequestDTO loginRequest);

    }

}
