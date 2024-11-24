using ems.application.Interfaces.IContext;
using ems.domain.Entity;

using Microsoft.EntityFrameworkCore;

namespace ems.persistance.Data.Context
{
    public class EmsDbContext : DbContext, IEmsDbContext
    {

      
        public virtual DbSet<AttendanceDeviceType> AttendanceDeviceTypes { get; set; }       
        public virtual DbSet<BasicMenu> BasicMenus { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; }

        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

        public virtual DbSet<EmployeeTypeBasicMenu> EmployeeTypeBasicMenus { get; set; }

        public virtual DbSet<LoginCredential> LoginCredentials { get; set; }

        public virtual DbSet<Operation> Operations { get; set; }

        public virtual DbSet<ProjectModuleDetail> ProjectModuleDetails { get; set; }

        public virtual DbSet<ProjectSubModuleDetail> ProjectSubModuleDetails { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; }

        public virtual DbSet<UserAttendanceSetting> UserAttendanceSettings { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<WorkstationType> WorkstationTypes { get; set; }      
        public DbSet<EmployeeTypeBasicMenu> EmployeeTypeBasicsMenus { get; set; }
       
        
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07AC087B1D");

                entity.ToTable("Employee", "emp");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.EmployementCode).HasMaxLength(50);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.MiddleName).HasMaxLength(100);
                entity.Property(e => e.OfficialEmail).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.EmployementType).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .HasConstraintName("FK_Employee_EmployeeType");
            });

            modelBuilder.Entity<WorkstationType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_WorkstationMode");

                entity.ToTable("WorkstationType", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Workstation)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC07DC059E47");

                entity.ToTable("UserRole", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.AssignedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(500);
                entity.Property(e => e.RemovedDateTime).HasColumnType("datetime");
                entity.Property(e => e.RoleStartDate).HasColumnType("datetime");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserAttendanceSetting>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserAtte__3214EC0740B39058");

                entity.ToTable("UserAttendanceSetting", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.GeofenceLatitude).HasColumnType("decimal(10, 8)");
                entity.Property(e => e.GeofenceLongitude).HasColumnType("decimal(10, 8)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsAllowed).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AttendanceDeviceType).WithMany(p => p.UserAttendanceSettings)
                    .HasForeignKey(d => d.AttendanceDeviceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAttendanceSetting_AttendanceDeviceType");

                //entity.HasOne(d => d.Employee).WithMany(p => p.use)
                //    .HasForeignKey(d => d.EmployeeId)
                //    .HasConstraintName("FK_UserAttendanceSetting_Employee");

                entity.HasOne(d => d.WorkstationType).WithMany(p => p.UserAttendanceSettings)
                    .HasForeignKey(d => d.WorkstationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAttendanceSetting_WorkstationType");
            });
            modelBuilder.Entity<RoleModuleAndPermission>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id).HasName("PK_RoleModuleAndPermission_Id");
                entity.ToTable("RoleModuleAndPermission", "emp");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.ImageIcon)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.SubModule).WithMany()
                    .HasForeignKey(d => d.SubModuleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RoleModuleAndPermission_SubModuleId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07B617DE9F");

                entity.ToTable("Role", "emp");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.RoleName).HasMaxLength(100);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjectSubModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectS__3214EC07F684FD7B");

                entity.ToTable("ProjectSubModuleDetail", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsSubModuleDisplayInUi)
                    .HasDefaultValue(true)
                    .HasColumnName("IsSubModuleDisplayInUI");
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.SubModuleName).HasMaxLength(255);
                entity.Property(e => e.SubModuleUrl)
                    .HasMaxLength(255)
                    .HasColumnName("SubModuleURL");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Module).WithMany(p => p.ProjectSubModuleDetails)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectSubModule_Module");
            });

            modelBuilder.Entity<ProjectModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectM__3214EC078CBD7460");

                entity.ToTable("ProjectModuleDetail", "emp");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.ModuleName).HasMaxLength(100);
                entity.Property(e => e.ModuleUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ModuleURL");
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });
            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC079906B6BF");

                entity.ToTable("Operation", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.OperationName).HasMaxLength(200);
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                // Configure the relationship with RoleModuleAndPermission
                entity.HasMany(e => e.RoleModuleAndPermissions)
                    .WithOne(d => d.Operations)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleModuleAndPermission_Operation");
            });


            modelBuilder.Entity<RoleModuleAndPermission>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_RoleModuleAndPermission_Id");

                entity.ToTable("RoleModuleAndPermission", "emp");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.ImageIcon)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.SubModule).WithMany(p => p.RoleModuleAndPermissions)
                    .HasForeignKey(d => d.SubModuleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RoleModuleAndPermission_SubModuleId");
            });
            modelBuilder.Entity<LoginCredential>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LoginCre__3214EC07E4FA9116");

                entity.ToTable("LoginCredential", "emp");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.IpAddress).HasMaxLength(255);
                entity.Property(e => e.LoginId).HasMaxLength(255);
                entity.Property(e => e.MacAddress).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                //entity.HasOne(d => d.Employee).WithMany(p => p.LoginCredentials)
                //    .HasForeignKey(d => d.EmployeeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_LoginCredential_Employee");
            });
            modelBuilder.Entity<BasicMenu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_BasicMenu_Id");

                entity.ToTable("BasicMenu", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.MenuName).HasMaxLength(100);
                entity.Property(e => e.MenuUrl)
                    .HasMaxLength(255)
                    .HasColumnName("MenuURL");
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                    .HasForeignKey(d => d.ParentMenuId)
                    .HasConstraintName("FK_BasicMenu_ParentMenu");
            });

            /*
       modelBuilder.Entity<Attendance>(entity =>
       {
           entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07DE9CBDF2");

           entity.ToTable("Attendance", "emp");

           entity.Property(e => e.AddedDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
       });

       modelBuilder.Entity<AttendanceDeviceType>(entity =>
       {
           entity.ToTable("AttendanceDeviceType", "emp");

           entity.Property(e => e.AddedDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.DeviceType).HasMaxLength(50);
           entity.Property(e => e.IsDeviceRegister).HasDefaultValue(false);
           entity.Property(e => e.Remark).HasMaxLength(255);
           entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
       });

       modelBuilder.Entity<AttendanceHistory>(entity =>
       {
           entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07C25A0FE6");

           entity.ToTable("AttendanceHistory", "emp");

           entity.Property(e => e.AddedDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.InTime).HasColumnType("datetime");
           entity.Property(e => e.OutTime).HasColumnType("datetime");
           entity.Property(e => e.Remarks).HasMaxLength(255);
           entity.Property(e => e.Status).HasMaxLength(20);
           entity.Property(e => e.TotalBreakHours).HasColumnType("decimal(5, 2)");
           entity.Property(e => e.TotalWorkHours).HasColumnType("decimal(5, 2)");
           entity.Property(e => e.UpdatedDateTime)
               .HasDefaultValueSql("(NULL)")
               .HasColumnType("datetime");

           entity.HasOne(d => d.Employee).WithMany(p => p.AttendanceHistories)
               .HasForeignKey(d => d.EmployeeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_AttendanceHistory_Employee");
       });

       modelBuilder.Entity<AttendanceRequest>(entity =>
       {
           entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07DFF12B49");

           entity.ToTable("AttendanceRequest", "emp");

           entity.Property(e => e.AddedDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
           entity.Property(e => e.Remark).HasMaxLength(255);
           entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
       });



       modelBuilder.Entity<Department>(entity =>
       {
           entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07B823F627");

           entity.ToTable("Department", "emp");

           entity.Property(e => e.AddedDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.DepartmentName).HasMaxLength(255);
           entity.Property(e => e.Description).HasMaxLength(500);
           entity.Property(e => e.IsActive).HasDefaultValue(true);
           entity.Property(e => e.Remark).HasMaxLength(200);
           entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

           entity.HasOne(d => d.ParentDepartment).WithMany(p => p.InverseParentDepartment)
               .HasForeignKey(d => d.ParentDepartmentId)
               .HasConstraintName("FK_ParentDepartment");
       });

       modelBuilder.Entity<DepartmentModule>(entity =>
       {
           entity
               .HasNoKey()
               .ToTable("DepartmentModule", "emp");

           entity.Property(e => e.CreatedDate).HasColumnType("datetime");
           entity.Property(e => e.ForPlatformRemark)
               .HasMaxLength(255)
               .IsUnicode(false);
           entity.Property(e => e.Id).ValueGeneratedOnAdd();
           entity.Property(e => e.IsActive).HasDefaultValue(true);
           entity.Property(e => e.ModuleName)
               .HasMaxLength(255)
               .IsUnicode(false);
           entity.Property(e => e.Remark)
               .HasMaxLength(255)
               .IsUnicode(false);
           entity.Property(e => e.SubModuleName)
               .HasMaxLength(255)
               .IsUnicode(false);
           entity.Property(e => e.Technology)
               .HasMaxLength(255)
               .IsUnicode(false);
           entity.Property(e => e.TechnologyRemark)
               .HasMaxLength(255)
               .IsUnicode(false);
           entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

           entity.HasOne(d => d.Department).WithMany()
               .HasForeignKey(d => d.DepartmentId)
               .HasConstraintName("FK__Departmen__Depar__3587F3E0");
       });


       modelBuilder.Entity<EmployeeDailyAttendance>(entity =>
       {
           entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0703C9A4C1");

           entity.ToTable("EmployeeDailyAttendance", "emp");

           entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
           entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
           entity.Property(e => e.IsLate).HasDefaultValue(false);
           entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

           entity.HasOne(d => d.AttendanceDeviceType).WithMany(p => p.EmployeeDailyAttendances)
               .HasForeignKey(d => d.AttendanceDeviceTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_AttendanceDeviceType");

           entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDailyAttendances)
               .HasForeignKey(d => d.EmployeeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Employee");

           entity.HasOne(d => d.WorkstationType).WithMany(p => p.EmployeeDailyAttendances)
               .HasForeignKey(d => d.WorkstationTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_WorkstationType");
       });



       modelBuilder.Entity<EmployeeStatusHistory>(entity =>
       {
           entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC076A652FF6");

           entity.ToTable("EmployeeStatusHistory", "emp");

           entity.Property(e => e.AddedDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.ChangeDateTime)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
           entity.Property(e => e.IsActive).HasDefaultValue(true);
           entity.Property(e => e.Remark).HasMaxLength(200);
           entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

           entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeStatusHistories)
               .HasForeignKey(d => d.EmployeeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_EmployeeStatusHistory_Employee");

           entity.HasOne(d => d.NewEmployeeType).WithMany(p => p.EmployeeStatusHistoryNewEmployeeTypes)
               .HasForeignKey(d => d.NewEmployeeTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_EmployeeStatusHistory_NewEmployeeType");

           entity.HasOne(d => d.OldEmployeeType).WithMany(p => p.EmployeeStatusHistoryOldEmployeeTypes)
               .HasForeignKey(d => d.OldEmployeeTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_EmployeeStatusHistory_OldEmployeeType");
       });
       */
            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0701E8E042");

                entity.ToTable("EmployeeType", "emp");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.TypeName).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                //entity.HasOne(d => d.Role).WithMany(p => p.EmployeeTypes)
                //    .HasForeignKey(d => d.RoleId)
                //    .HasConstraintName("FK_EmployeeType_Role");
            });

            modelBuilder.Entity<EmployeeTypeBasicMenu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07335FD665");

                entity.ToTable("EmployeeTypeBasicMenu", "emp");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsMenuDisplayInUi).HasColumnName("IsMenuDisplayInUI");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                //entity.HasOne(d => d.BasicMenu).WithMany(p => p.EmployeeTypeBasicMenus)
                //    .HasForeignKey(d => d.BasicMenuId)
                //    .HasConstraintName("FK_EmployeeTypeBasicMenu_BasicMenu");

                //entity.HasOne(d => d.EmployeeType).WithMany(p => p.EmployeeTypeBasicMenus)
                //    .HasForeignKey(d => d.EmployeeTypeId)
                //    .HasConstraintName("FK_EmployeeTypeBasicMenu_EmployeeType");
            });




        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
