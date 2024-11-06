using ems.application.Interfaces.ILogger;
using ems.application.Interfaces.ITokenService;
using ems.infrastructure.Security;
using ems.infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ems.infrastructure
{
    public static class ServiceExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Registering the logger service
           

            // Registering the token service with both configuration and logger
            services.AddScoped<ITokenService, TokenService>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<TokenService>>();
                return new TokenService(configuration, logger);
            });

            services.AddScoped<ILoggerService, LoggerService>();

        }
    }
}
