using ems.application.Interfaces;
using ems.application.Interfaces.IContext;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ems.persistance
{
    public static class ServiceExtentions
    {


        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Debugging: Connection String Check Karein
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection String is null or empty!");
            }
            // ✅ DbContext with EF Core Logging + Serilog
            services.AddDbContext<WorkforceDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging() // Logs parameter values
                       .EnableDetailedErrors()       // Logs detailed exception info
                     .LogTo(Console.WriteLine, LogLevel.Information) // 👈 EF logs to console
                     .LogTo(Log.Logger.Information, LogLevel.Information) // 👈 EF logs to Serilog
            );

            // ✅ Correct Scoped DbContext Injection
            services.AddScoped<IWorkforceDbContext>(provider => provider.GetRequiredService<WorkforceDbContext>());

            // ✅ Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ✅ Repositories should be Scoped (instead of Transient)
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<INewTokenRepository, NewTokenRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserLoginReopsitory, UserLoginReopsitory>();  // **FIXED**
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();
            services.AddScoped<IEmployeeTypeBasicMenuRepository, EmployeeTypeBasicMenuRepository>();
            services.AddScoped<IUserRolesPermissionOnModuleRepository, UserRolesPermissionOnModuleRepository>();
            services.AddScoped<ICandidateRegistrationRepository, CandidateRegistrationRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITravelRepository, TravelRepository>();
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<ICandidateCategorySkillRepository, CandidateCategorySkillRepository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddTransient<ITenantEmailConfigRepository, TenantEmailConfigRepository>();
            services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();

            services.AddTransient<ITenantSubscriptionRepository, TenantSubscriptionRepository>();
            services.AddTransient<IPlanModuleMappingRepository, PlanModuleMappingRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<ITenantModuleConfigurationRepository, TenantModuleConfigurationRepository>();

          

            // Debugging Logs
            Console.WriteLine("✅ Persistence Layer Configured Successfully.");
        }
    }

}
