using ems.application.Constants;
using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class UserLoginReopsitory : IUserLoginReopsitory
    {
        private WorkforceDbContext context;

        public UserLoginReopsitory(WorkforceDbContext context)
        {
            this.context = context;
        }

         public async Task<bool> IsValidUserAsync(long empId)
        {
            // Fetch user details based on LoginId from the repository
                var user = await context.LoginCredentials.FirstOrDefaultAsync(u => u.EmployeeId == empId && u.IsActive == true);
            // name _unitOfWork is not exist error aa rahi hai
                       
            var rr = user;

                        // Check if user exists and if the password matches
            if (user == null)
            {
                return false;
                 
            }


            // Return success response with token
            return true;
        }

        public async Task<LoginResponseDTO> AuthenticateUser(LoginRequestDTO loginRequest)
        {
            // Fetch user details based on LoginId from the repository
                var user = await context.LoginCredentials.FirstOrDefaultAsync(u => u.LoginId == loginRequest.LoginId);
            // name _unitOfWork is not exist error aa rahi hai
                       
            var rr = user;

                        // Check if user exists and if the password matches
            if (user == null || !VerifyPassword(loginRequest.Password, user.Password))
            {
                return new LoginResponseDTO
                {
                    Success = ConstantValues.fail

                };
            }

            // Generate token if needed (e.g., using JWT)
            string token = GenerateJwtToken(user);

            // Return success response with token
            return new LoginResponseDTO
            {
                Success = ConstantValues.isSucceeded,
                Token = token,               
                Id = user.EmployeeId

            };
        }

        // This is a placeholder for the password verification logic
        private bool VerifyPassword(string providedPassword, string storedPassword)
        {
            // Here, you would hash the providedPassword and compare with storedPassword
            // This is a basic example, assuming plain-text comparison (not secure for production)
            return providedPassword == storedPassword;
        }

        // Placeholder for JWT token generation
        private string GenerateJwtToken(LoginCredential user)
        {
            // Code to generate JWT token based on user information
            return "GeneratedTokenHere"; // Replace with actual JWT generation logic
        }

    }
}
