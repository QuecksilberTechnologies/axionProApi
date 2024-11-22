using ems.application.Interfaces.IContext;
using ems.domain.Entity.BasicMenuInfo;
using ems.domain.Entity.EmployeeModule;
using ems.domain.Entity.Masters.ModuleOperation;
using ems.domain.Entity.Masters.ProjectModuleInfo;
using ems.domain.Entity.Masters.RoleInfo;
using ems.domain.Entity.RoleModulePermission;
using ems.domain.Entity.UserCredential;
using ems.domain.Entity.UserRoleModule;
using ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig;
 
using ems.persistance.Data.Configurations.EmployeeConfig;
using ems.persistance.Data.Configurations.EmployeeTypeConfig;
using ems.persistance.Data.Configurations.LoginDetailConfig;
using ems.persistance.Data.Configurations.OperationConfig;
using ems.persistance.Data.Configurations.UserRollConfig;
using Microsoft.EntityFrameworkCore;

namespace ems.persistance.Data.Context
{
    public class EmsDbContext : DbContext, IEmsDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeTypeBasicMenu> EmployeeTypeBasicsMenus { get; set; }
        public DbSet<LoginCredential> LoginCredentials { get; set; } // New DbSet for LoginCredential

          // New DbSet for LoginCredential
      //  public DbSet<UserRole> UserRoll { get; set; } // New DbSet for LoginCredential
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; }
              

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<ProjectModuleDetail> ProjectModuleDetails { get; set; }
        public DbSet<ProjectSubModuleDetail> ProjectSubModuleDetails { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee","emp"); // or "Employees" if plural
           
          //  modelBuilder.Entity<Employee>().ToTable("Employee","emp"); // or "Employees" if plural
           
           // modelBuilder.Entity<LoginCredential>().ToTable("LoginCredential", "emp"); // Specify schema and table for LoginCredential
            base.OnModelCreating(modelBuilder);
            // Configure LoginCredential entity
            modelBuilder.ApplyConfiguration(new LoginCredentialConfiguration());   
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleModuleAndPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeTypeBasicMenuConfiguration());
            modelBuilder.ApplyConfiguration(new BasicMenuConfiguration());
            modelBuilder.ApplyConfiguration(new OperationConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectSubModuleDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectModuleDetailConfiguration());



        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
