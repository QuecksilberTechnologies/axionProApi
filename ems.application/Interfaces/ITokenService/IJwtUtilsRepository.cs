using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.ITokenService
{
    public interface IJwtUtilsRepository
    {
       // IDictionary<string, string> ValidateToken(string token, string secretKey);
        string GetEmailFromToken(string token);
    }
}
