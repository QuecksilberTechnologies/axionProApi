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
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            //register services
            services.AddDbContext<EmsDbContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
                ));

             services.AddTransient<IEmsDbContext, EmsDbContext>();
            // Register UnitOfWork with Scoped lifetime
             services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register repositories
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IUserLoginReopsitory, UserLoginReopsitory>();
            services.AddTransient<ICommonMenuRepository, CommonMenuRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            // services.AddTransient<ICompanyRepository, CompanyRepository>();


            //services.AddIdentityCore<ApplicationUser>()
            //    .AddRoles<ApplicationRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            //services.AddTransient<IAccountService, AccountService>();
            ////Seeds roles and users
            //DefaultRoles.SeedRolesAsync(services.BuildServiceProvider()).Wait();
            //DefaultUsers.SeedUsersAsync(services.BuildServiceProvider()).Wait();
        }
    }
}
