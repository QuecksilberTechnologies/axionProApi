using ems.application.DTOs.UserLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.ITokenService
{
    // ITokenService.cs (Application Layer)
    public interface INewTokenRepository
    {
      public Task <string> GenerateToken(string userId);
       
            bool ValidateToken(string token);
        public Task<string>  GenerateRefreshToken();
        // ✅ Naye methods for extracting info
        Task<string> GetUserInfoFromToken(string token);
        DateTime? GetExpiryFromToken(string token);

    }


}
