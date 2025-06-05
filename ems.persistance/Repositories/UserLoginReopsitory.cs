using ems.application.Constants;
using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly WorkforceDbContext _context;
        private readonly ILogger<UserLoginReopsitory> _logger;

        public UserLoginReopsitory(WorkforceDbContext context, ILogger<UserLoginReopsitory> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<LoginCredential?> AuthenticateUser(LoginRequestDTO loginRequest)
        {
            try
            {
                _logger.LogInformation("Authenticating user with LoginId: {LoginId}", loginRequest.LoginId);

                LoginCredential? user = await _context.LoginCredentials
                    .FirstOrDefaultAsync(u => u.LoginId == loginRequest.LoginId);

                if (user == null)
                {
                    _logger.LogWarning("Login failed: User not found for LoginId: {LoginId}", loginRequest.LoginId);
                    return null;
                }

                if (!VerifyPassword(loginRequest.Password, user.Password))
                {
                    _logger.LogWarning("Login failed: Incorrect password for LoginId: {LoginId}", loginRequest.LoginId);
                    return null;
                }

                _logger.LogInformation("User authenticated successfully: {LoginId}", loginRequest.LoginId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating user with LoginId: {LoginId}", loginRequest.LoginId);
                throw; // Rethrowing the exception to ensure proper handling at higher levels
            }
        }

        public async Task<long> CreateUser(LoginCredential loginRequest)
        {
            try
            {
                if (_context == null)
                {
                    _logger?.LogError("DbContext is null in CreateUser.");
                    throw new ArgumentNullException(nameof(_context), "DbContext is not initialized.");
                }

               

                await _context.LoginCredentials.AddAsync(loginRequest); // Add LoginCredential
                await _context.SaveChangesAsync(); // Save changes

                _logger?.LogInformation("User created successfully with ID: {UserId}", loginRequest.Id);

                return loginRequest.Id; // Return auto-generated ID
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while creating user.");
                throw;
            }
        }

     

        private bool VerifyPassword(string providedPassword, string storedPassword)
        {
            // Secure hashing and comparison logic should be implemented here
            return providedPassword == storedPassword;
        }
    }

}
