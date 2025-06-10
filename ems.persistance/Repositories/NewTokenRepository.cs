using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.ITokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class NewTokenRepository : INewTokenRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<NewTokenRepository> _logger;

    public NewTokenRepository(IConfiguration configuration, ILogger<NewTokenRepository> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<string> GetUserInfoFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWTSettings:Secret"]);

        try
        {
            var principal = handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid")?.Value;
            var emailId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var expiry = (validatedToken as JwtSecurityToken)?.ValidTo;

            var userInfo = new
            {
                UserId = userId,
                Email = emailId,
                Expiry = expiry?.ToString("o"),
                IsExpired = expiry < DateTime.UtcNow
            };

            return JsonConvert.SerializeObject(userInfo);
        }
        catch (Exception ex)
        {
            // Agar token galat hai to null ya error message return karo
            var errorResponse = new
            {
                UserId = (string)null,
                Email = (string)null,
                Expiry = (string)null,
                IsExpired = true,
                Error = "Invalid or tampered token."
            };

            return JsonConvert.SerializeObject(errorResponse);
        }
    }

    public DateTime? GetExpiryFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.ValidTo; // UTC format
    }


    public async Task<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:Secret"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // Optional: set true if issuer must match
                ValidateAudience = false, // Optional: set true if audience must match
                ClockSkew = TimeSpan.Zero // No extra buffer time
            }, out SecurityToken validatedToken);

            return true; // Token is valid
        }
        catch
        {
            return false; // Token invalid or expired
        }
    }

    public async Task<string> GenerateToken(string userId)
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

            // Adding issuer and audience claims
            var issuer = _configuration["JWTSettings:Issuer"];
            var audience = _configuration["JWTSettings:Audience"];

            // Parse the TokenLifetime from the config (in TimeSpan format)
            var tokenLifetime = TimeSpan.Parse(_configuration["JWTSettings:TokenLifetime"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                // Add any other claims here as needed
            }),
                Expires = DateTime.UtcNow.Add(tokenLifetime), // Set token expiry
                Issuer = issuer, // Set issuer
                Audience = audience, // Set audience
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token generation failed for LoginId: {LoginId}", userId);
            return null; // If token generation fails, return null
        }
    }


}
