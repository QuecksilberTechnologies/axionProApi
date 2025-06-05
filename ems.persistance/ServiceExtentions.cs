using ems.application.Interfaces;
using ems.application.Interfaces.IContext;
using ems.application.Interfaces.IRepositories;
using ems.application.Interfaces.ITokenService;
using ems.persistance.Data.Context;
using ems.persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            // ✅ DbContext with Scoped Lifetime
            services.AddDbContext<WorkforceDbContext>(options =>
                options.UseSqlServer(connectionString));

            // ✅ Correct Injection for WorkforceDbContext
            services.AddScoped<IWorkforceDbContext>(provider => provider.GetRequiredService<WorkforceDbContext>());

            // ✅ Unit of Work should be Scoped
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
           

            // Debugging Logs
            Console.WriteLine("✅ Persistence Layer Configured Successfully.");
        }
    }

}
