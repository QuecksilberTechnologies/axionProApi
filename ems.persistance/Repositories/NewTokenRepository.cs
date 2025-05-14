using ems.application.DTOs.UserLogin;
using ems.application.Interfaces.ITokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        throw new NotImplementedException();
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
