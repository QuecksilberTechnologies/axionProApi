using ems.application.Interfaces.IContext;
using ems.domain.Entity.CommonMenu;
using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.Masters.RoleInfo;
using ems.domain.Entity.UserCredential;
using ems.domain.Entity.UserRoleModule;
using ems.persistance.Data.Configurations.CommonAndRoleBaseMenuConfig;
 
using ems.persistance.Data.Configurations.LoginDetailConfig;
using ems.persistance.Data.Configurations.UserRollConfig;
using Microsoft.EntityFrameworkCore;

namespace ems.persistance.Data.Context
{
    public class EmsDbContext : DbContext, IEmsDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LoginCredential> LoginCredentials { get; set; } // New DbSet for LoginCredential
        public DbSet<CommonMenu> CommonMenus { get; set; } // New DbSet for LoginCredential
      //  public DbSet<UserRole> UserRoll { get; set; } // New DbSet for LoginCredential
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Role> Roles { get; set; }
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee","emp"); // or "Employees" if plural
           
           // modelBuilder.Entity<LoginCredential>().ToTable("LoginCredential", "emp"); // Specify schema and table for LoginCredential
            base.OnModelCreating(modelBuilder);
            // Configure LoginCredential entity
            modelBuilder.ApplyConfiguration(new LoginCredentialConfiguration());
            modelBuilder.ApplyConfiguration(new CommonMenuConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
