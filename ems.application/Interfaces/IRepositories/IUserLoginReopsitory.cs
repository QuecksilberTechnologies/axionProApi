using ems.application.DTOs.UserLogin;
using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IUserLoginReopsitory
    {
        Task<LoginCredential> AuthenticateUser(LoginRequestDTO loginRequest);
        Task<long> CreateUser(LoginCredential loginRequest);
        Task<bool> UpdateNewPassword(LoginCredential setRequest);
        Task<LoginCredential> GetEmployeeIdByUserLogin(string userLoing);
       
    }

}
