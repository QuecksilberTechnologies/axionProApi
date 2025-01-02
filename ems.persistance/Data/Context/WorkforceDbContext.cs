using ems.application.Interfaces.IContext;
using ems.domain.Entity;

using Microsoft.EntityFrameworkCore;

namespace ems.persistance.Data.Context
{
    public class WorkforceDbContext : DbContext, IWorkforceDbContext
    {

        public virtual DbSet<Attendance> Attendances { get; set; }

        public virtual DbSet<AttendanceDeviceType> AttendanceDeviceTypes { get; set; }

        public virtual DbSet<AttendanceHistory> AttendanceHistories { get; set; }

        public virtual DbSet<AttendanceRequest> AttendanceRequests { get; set; }

        public virtual DbSet<BasicMenu> BasicMenus { get; set; }

        public virtual DbSet<Candidate> Candidates { get; set; }

        public virtual DbSet<CandidateDepartmentModuleSkill> CandidateDepartmentModuleSkills { get; set; }

        public virtual DbSet<CandidateHistory> CandidateHistories { get; set; }

       public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<DepartmentModule> DepartmentModules { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; }

        public virtual DbSet<EmployeeStatusHistory> EmployeeStatusHistories { get; set; }

        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

        public virtual DbSet<EmployeeTypeBasicMenu> EmployeeTypeBasicMenus { get; set; }

        public virtual DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }

        public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }

       
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
        public DbSet<Category> Categories { get; set; }
       
        
        public WorkforceDbContext(DbContextOptions<WorkforceDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07DE9CBDF2");

                entity.ToTable("Attendance", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AttendanceDeviceType>(entity =>
            {
                entity.ToTable("AttendanceDeviceType", "AxionPro");

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

                entity.ToTable("AttendanceHistory", "AxionPro");

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
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07747F8170");

                entity.ToTable("Category", "AxionPro");

                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.ParentId).HasColumnName("ParentID");
                entity.Property(e => e.Remark).HasMaxLength(50);
                entity.Property(e => e.Tags).HasMaxLength(255);

                entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Category_Parent");
            });
            modelBuilder.Entity<AttendanceRequest>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07DFF12B49");

                entity.ToTable("AttendanceRequest", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<BasicMenu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_BasicMenu_Id");

                entity.ToTable("BasicMenu", "AxionPro");

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

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07B823F627");

                entity.ToTable("Department", "AxionPro");

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
                    .ToTable("DepartmentModule", "AxionPro");

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

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07AC087B1D");

                entity.ToTable("Employee", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.EmployementCode).HasMaxLength(50);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.MiddleName).HasMaxLength(100);
                entity.Property(e => e.OfficialEmail).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .HasConstraintName("FK_Employee_EmployeeType");
            });

            modelBuilder.Entity<EmployeeDailyAttendance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0703C9A4C1");

                entity.ToTable("EmployeeDailyAttendance", "AxionPro");

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

                entity.ToTable("EmployeeStatusHistory", "AxionPro");

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

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0701E8E042");

                entity.ToTable("EmployeeType", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.TypeName).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Role).WithMany(p => p.EmployeeTypes)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_EmployeeType_Role");
            });

            modelBuilder.Entity<EmployeeTypeBasicMenu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07335FD665");

                entity.ToTable("EmployeeTypeBasicMenu", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsMenuDisplayInUi).HasColumnName("IsMenuDisplayInUI");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.BasicMenu).WithMany(p => p.EmployeeTypeBasicMenus)
                    .HasForeignKey(d => d.BasicMenuId)
                    .HasConstraintName("FK_EmployeeTypeBasicMenu_BasicMenu");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.EmployeeTypeBasicMenus)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .HasConstraintName("FK_EmployeeTypeBasicMenu_EmployeeType");
            });

            modelBuilder.Entity<InterviewFeedback>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC070C3DB2B6");

                entity.ToTable("InterviewFeedback", "AxionPro");

                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
                entity.Property(e => e.ReapplyAfter).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Candidate).WithMany(p => p.InterviewFeedbacks)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interview__Candi__0C1BC9F9");

                entity.HasOne(d => d.InterviewSchedule).WithMany(p => p.InterviewFeedbacks)
                    .HasForeignKey(d => d.InterviewScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interview__Inter__0B27A5C0");
            });
 

            modelBuilder.Entity<LoginCredential>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LoginCre__3214EC07E4FA9116");

                entity.ToTable("LoginCredential", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.IpAddress).HasMaxLength(255);
                entity.Property(e => e.LoginId).HasMaxLength(255);
                entity.Property(e => e.MacAddress).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Employee).WithMany(p => p.LoginCredentials)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginCredential_Employee");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC079906B6BF");

                entity.ToTable("Operation", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.OperationName).HasMaxLength(200);
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjectModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectM__3214EC078CBD7460");

                entity.ToTable("ProjectModuleDetail", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.ModuleName).HasMaxLength(100);
                entity.Property(e => e.ModuleUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ModuleURL");
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjectSubModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectS__3214EC07F684FD7B");

                entity.ToTable("ProjectSubModuleDetail", "AxionPro");

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

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07B617DE9F");

                entity.ToTable("Role", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.RoleName).HasMaxLength(100);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleModuleAndPermission>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_RoleModuleAndPermission_Id");

                entity.ToTable("RoleModuleAndPermission", "AxionPro");

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

            modelBuilder.Entity<UserAttendanceSetting>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserAtte__3214EC0740B39058");

                entity.ToTable("UserAttendanceSetting", "AxionPro");

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

                entity.HasOne(d => d.Employee).WithMany(p => p.UserAttendanceSettings)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_UserAttendanceSetting_Employee");

                entity.HasOne(d => d.WorkstationType).WithMany(p => p.UserAttendanceSettings)
                    .HasForeignKey(d => d.WorkstationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAttendanceSetting_WorkstationType");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC07DC059E47");

                entity.ToTable("UserRole", "AxionPro");

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

            modelBuilder.Entity<WorkstationType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_WorkstationMode");

                entity.ToTable("WorkstationType", "AxionPro");

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

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC07A7BD071A");

                entity.ToTable("Candidate", "AxionPro");

                entity.HasIndex(e => e.PhoneNumber, "UQ__Candidat__85FB4E38B864B790").IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Candidat__A9D1053424EBF1F0").IsUnique();

                entity.HasIndex(e => e.Aadhaar, "UQ__Candidat__C4B3336970F173B9").IsUnique();

                entity.HasIndex(e => e.Pan, "UQ__Candidat__C577943DC86B77C1").IsUnique();

                entity.HasIndex(e => e.CandidateReferenceCode, "UQ__Candidat__CF22B81AC2D2FB85").IsUnique();

                entity.Property(e => e.Aadhaar).HasMaxLength(12);
                entity.Property(e => e.AppliedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.CandidateReferenceCode).HasMaxLength(20);
                entity.Property(e => e.CurrentCompany).HasMaxLength(200);
                entity.Property(e => e.CurrentLocation).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.ExpectedSalary).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ExperienceYears).HasColumnType("decimal(4, 1)");
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.LastUpdatedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Pan)
                    .HasMaxLength(10)
                    .HasColumnName("PAN");
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.ResumeUrl).HasMaxLength(500);
            });

            modelBuilder.Entity<CandidateDepartmentModuleSkill>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC0758D57F40");

                entity.ToTable("CandidateDepartmentModuleSkills", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.CandidateDepartmentModuleSkill1).HasColumnName("CandidateDepartmentModuleSkill");

                entity.HasOne(d => d.CandidateDepartmentModuleSkill1Navigation).WithMany(p => p.InverseCandidateDepartmentModuleSkill1Navigation)
                    .HasForeignKey(d => d.CandidateDepartmentModuleSkill1)
                    .HasConstraintName("FK_CandidateDepartmentModuleSkills_Self");

                entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateDepartmentModuleSkills)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateDepartmentModuleSkills_Candidate");
            });

            modelBuilder.Entity<CandidateHistory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC0731CB3F59");

                entity.ToTable("CandidateHistory", "AxionPro");

                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.ReapplyAllowedAfter).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateHistories)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Candi__10E07F16");
            });


        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
