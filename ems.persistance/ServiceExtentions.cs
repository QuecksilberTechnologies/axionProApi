using ems.application.Interfaces.Context;
using ems.application.Interfaces.Repositories;
using ems.application.Interfaces.UnitOfWork;
using ems.persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;

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
            services.AddScoped<IUnitOfWork,UnitOfWork.UnitOfWork>();
            
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();

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
