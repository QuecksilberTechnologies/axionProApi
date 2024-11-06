using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.ITokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ems.infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GenerateToken(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Ensure the secret key is retrieved correctly
                var jwtKey = _configuration["JWTSettings:Secret"];
                if (string.IsNullOrEmpty(jwtKey))
                {
                    throw new ArgumentNullException("JWTSettings:Secret", "JWT key cannot be null or empty.");
                }

                var key = Encoding.UTF8.GetBytes(jwtKey);

                // Check if the key size is sufficient
                if (key.Length < 32) // HMAC SHA-256 requires a key of at least 256 bits (32 bytes)
                {
                    throw new ArgumentException("JWT key must be at least 32 bytes long for HMAC SHA-256.");
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginRequestDTO.LoginId.ToString()),
                        // Add additional claims here if necessary
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token generation failed for LoginId: {LoginId}", loginRequestDTO.LoginId);
                return null; // If token generation fails, return null
            }
        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
