using ems.application.Interfaces.IEmail;
using ems.application.Interfaces.ILogger;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.infrastructure.Logging;
 
using ems.infrastructure.Security;
using ems.persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ems.domain.Entity;
using ems.infrastructure.MailService;

namespace ems.infrastructure
{
    public static class ServiceExtentions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Registering the logger service
           

            // Registering the token service with both configuration and logger
            //services.AddScoped<INewTokenRepository, NewTokenService>(provider =>
            //{
            //    var logger = provider.GetRequiredService<ILogger<NewTokenService>>();
            //    return new NewTokenService(configuration, logger);
            //});

            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IEmailService,EmailService>();
                 
            services.AddScoped<ITenantEmailConfigRepository, TenantEmailConfigRepository>();
          




        }
    }
}
