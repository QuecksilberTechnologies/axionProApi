using ems.application.DTOs.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.ITokenService
{
    // ITokenService.cs (Application Layer)
    public interface ITokenService
    {
        string GenerateToken(LoginRequestDTO loginRequestDTO);
       
            bool ValidateToken(string token);
        

    }

}
