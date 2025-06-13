using ems.application.DTOs.Operation;
using ems.application.Interfaces.IContext;
using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
 


using Microsoft.EntityFrameworkCore;

namespace ems.persistance.Data.Context
{
    public class WorkforceDbContext : DbContext, IWorkforceDbContext
    {

        public virtual DbSet<AccoumndationAllowancePolicyByDesignation> AccoumndationAllowancePolicyByDesignations { get; set; }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<CommonItem> CommonItems { get; set; }
        public virtual DbSet<RoleModulePermission> RoleModulePermissions { get; set; }
        public virtual DbSet<CheckOperationPermissionRequestDTO> HasAccessOperations { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
      
        public virtual DbSet<AssetAssignment> AssetAssignments { get; set; }

        public virtual DbSet<AssetHistory> AssetHistories { get; set; }

        public virtual DbSet<AssetStatus> AssetStatuses { get; set; }

        public virtual DbSet<AssetType> AssetTypes { get; set; }

        public virtual DbSet<AssignmentStatus> AssignmentStatuses { get; set; }

        public virtual DbSet<Attendance> Attendances { get; set; }

        public virtual DbSet<AttendanceDeviceType> AttendanceDeviceTypes { get; set; }

        public virtual DbSet<AttendanceHistory> AttendanceHistories { get; set; }

        public virtual DbSet<AttendanceRequest> AttendanceRequests { get; set; }

        public virtual DbSet<BasicMenu> BasicMenus { get; set; }

        public virtual DbSet<Candidate> Candidates { get; set; }

        public virtual DbSet<CandidateCategorySkill> CandidateCategorySkills { get; set; }

        public virtual DbSet<CandidateHistory> CandidateHistories { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<ClientType> ClientTypes { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<DepartmentModule> DepartmentModules { get; set; }

        public virtual DbSet<Designation> Designations { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TenantProfile> TenantProfiles { get; set; }

        public virtual DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }

        public virtual DbSet<EmployeeCategorySkill> EmployeeCategorySkills { get; set; }

        public virtual DbSet<EmployeeDailyAttendance> EmployeeDailyAttendances { get; set; }

        public virtual DbSet<EmployeeDependent> EmployeeDependents { get; set; }

        public virtual DbSet<EmployeeEducation> EmployeeEducations { get; set; }

        public virtual DbSet<EmployeeExperience> EmployeeExperiences { get; set; }

        public virtual DbSet<EmployeePersonalDetail> EmployeePersonalDetails { get; set; }

        public virtual DbSet<EmployeePolicy> EmployeePolicies { get; set; }

        public virtual DbSet<EmployeeStatusHistory> EmployeeStatusHistories { get; set; }

        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

        public virtual DbSet<EmployeeTypeBasicMenu> EmployeeTypeBasicMenus { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }

        public virtual DbSet<InterviewPanel> InterviewPanels { get; set; }

        public virtual DbSet<InterviewPanelMember> InterviewPanelMembers { get; set; }

        public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }

        public virtual DbSet<InterviewSdule> InterviewSdules { get; set; }

        public virtual DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public virtual DbSet<LeaveCarryForwardRule> LeaveCarryForwardRules { get; set; }

        public virtual DbSet<LeavePolicyByDesignation> LeavePolicyByDesignations { get; set; }

        public virtual DbSet<LeaveType> LeaveTypes { get; set; }

        public virtual DbSet<LoginCredential> LoginCredentials { get; set; }

        public virtual DbSet<MealAllowancePolicyByDesignation> MealAllowancePolicyByDesignations { get; set; }

        public virtual DbSet<Operation> Operations { get; set; }

        public virtual DbSet<PolicyType> PolicyTypes { get; set; }

        public virtual DbSet<ProjectChildModuleDetail> ProjectChildModuleDetails { get; set; }

        public virtual DbSet<ProjectModuleDetail> ProjectModuleDetails { get; set; }

        public virtual DbSet<ProjectSubModuleDetail> ProjectSubModuleDetails { get; set; }

        //public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

       

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

        public virtual DbSet<RoleModuleAndPermission> RoleModuleAndPermissions { get; set; }

        public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }

        public virtual DbSet<ServiceProviderContact> ServiceProviderContacts { get; set; }

        public virtual DbSet<Tender> Tenders { get; set; }

        public virtual DbSet<TenderProject> TenderProjects { get; set; }

        public virtual DbSet<TenderService> TenderServices { get; set; }

        public virtual DbSet<TenderServiceHistory> TenderServiceHistories { get; set; }

        public virtual DbSet<TenderServiceProvider> TenderServiceProviders { get; set; }

        public virtual DbSet<TenderServiceSpecification> TenderServiceSpecifications { get; set; }

        public virtual DbSet<TenderServiceType> TenderServiceTypes { get; set; }

        public virtual DbSet<TenantEmailConfig> TenantEmailConfigs { get; set; }
        public virtual DbSet<TenderStatus> TenderStatuses { get; set; }

        public virtual DbSet<TravelAllowancePolicyByDesignation> TravelAllowancePolicyByDesignations { get; set; }

        public virtual DbSet<TravelMode> TravelModes { get; set; }

        public virtual DbSet<UserAttendanceSetting> UserAttendanceSettings { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<WorkstationType> WorkstationTypes { get; set; }



        public WorkforceDbContext(DbContextOptions<WorkforceDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccoumndationAllowancePolicyByDesignation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Accoumnd__3214EC071BDF4022");

                entity.ToTable("AccoumndationAllowancePolicyByDesignation", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.FixedStayAllowance)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsMetro).HasDefaultValue(false);
                entity.Property(e => e.IsSoftDelete).HasDefaultValue(false);
                entity.Property(e => e.MetroBonus)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.MinDaysRequired).HasDefaultValue(0);
                entity.Property(e => e.RequiredDocuments).HasColumnType("text");
                entity.Property(e => e.SoftDeleteDateTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Designation).WithMany(p => p.AccoumndationAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accoumnda__Desig__11158940");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.AccoumndationAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accoumnda__Emplo__1209AD79");

                entity.HasOne(d => d.PolicyType).WithMany(p => p.AccoumndationAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.PolicyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accoumnda__Polic__12FDD1B2");
            });
            modelBuilder.Entity<CommonItem>().HasNoKey(); // 

            
            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__EmailTem__3214EC072FE49A64");

                entity.ToTable("EmailTemplate", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.AddedFromIp)
                    .HasMaxLength(50)
                    .HasColumnName("AddedFromIP");
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.FromEmail).HasMaxLength(150);
                entity.Property(e => e.FromName).HasMaxLength(100);
                entity.Property(e => e.CcEmail).HasMaxLength(300);
                entity.Property(e => e.BccEmail).HasMaxLength(300);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.LanguageCode).HasMaxLength(10);
                entity.Property(e => e.Subject).HasMaxLength(250);
                entity.Property(e => e.TemplateCode).HasMaxLength(100);
                entity.Property(e => e.TemplateName).HasMaxLength(150);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedFromIp)
                    .HasMaxLength(50)
                    .HasColumnName("UpdatedFromIP");
            });


            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Asset__3214EC076178ABAE");

                entity.ToTable("Asset", "AxionPro");

                entity.HasIndex(e => e.SerialNumber, "UQ__Asset__048A00085990C20A").IsUnique();

                entity.HasIndex(e => e.Barcode, "UQ__Asset__177800D3C3732639").IsUnique();

                entity.HasIndex(e => e.Qrcode, "UQ__Asset__5B869AD9466F0980").IsUnique();

                entity.Property(e => e.AssetName).HasMaxLength(100);
                entity.Property(e => e.Barcode).HasMaxLength(100);
                entity.Property(e => e.Color).HasMaxLength(50);
                entity.Property(e => e.Company).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsAssigned).HasDefaultValue(false);
                entity.Property(e => e.IsRepairable).HasDefaultValue(true);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
                entity.Property(e => e.Qrcode)
                    .HasMaxLength(100)
                    .HasColumnName("QRCode");
                // Common Entities
                entity.ConfigureBaseEntity();
                entity.Property(e => e.SerialNumber).HasMaxLength(100);              
                entity.Property(e => e.WarrantyExpiryDate).HasColumnType("datetime");               
                entity.HasOne(d => d.AssetStatus).WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_AssetStatus");
                entity.HasOne(d => d.AssetType).WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_AssetType");

            });

            modelBuilder.Entity<AssetAssignment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AssetAss__3214EC07CFA2D85C");

                entity.ToTable("AssetAssignment", "AxionPro");

                entity.Property(e => e.ActualReturnDate).HasColumnType("datetime");
             
                entity.Property(e => e.AssetConditionAtAssign).HasMaxLength(255);
                entity.Property(e => e.AssetConditionAtReturn).HasMaxLength(255);
                entity.Property(e => e.AssignedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.ExpectedReturnDate).HasColumnType("datetime");
                entity.Property(e => e.IdentificationMethod).HasMaxLength(50);
                entity.Property(e => e.IdentificationValue).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Common Entities
                entity.ConfigureBaseEntity();

                entity.HasOne(d => d.Asset).WithMany(p => p.AssetAssignments)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetAssignment_Asset");

                entity.HasOne(d => d.AssignmentStatus).WithMany(p => p.AssetAssignments)
                    .HasForeignKey(d => d.AssignmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetAssignment_AssignmentStatus");

                entity.HasOne(d => d.Employee).WithMany(p => p.AssetAssignments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetAssignment_Employee");
            });

            modelBuilder.Entity<AssetHistory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AssetHis__3214EC07599816A6");

                entity.ToTable("AssetHistory", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.AssetConditionAtAssign).HasMaxLength(100);
                entity.Property(e => e.AssetConditionAtReturn).HasMaxLength(100);
                entity.Property(e => e.AssignedDate).HasColumnType("datetime");
                entity.Property(e => e.IdentificationMethod).HasMaxLength(50);
                entity.Property(e => e.IdentificationValue).HasMaxLength(255);
                entity.Property(e => e.IsScrapped).HasDefaultValue(false);
                entity.Property(e => e.Remarks).HasMaxLength(500);
                entity.Property(e => e.ReturnedDate).HasColumnType("datetime");
                entity.Property(e => e.ScrapDate).HasColumnType("datetime");
                entity.Property(e => e.ScrapReason).HasMaxLength(255);
                // Common Entities
                entity.ConfigureBaseEntity();
                entity.HasOne(d => d.AssignmentStatus).WithMany(p => p.AssetHistories)
                    .HasForeignKey(d => d.AssignmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssetHist__Assig__7AF13DF7");

                entity.HasOne(d => d.Employee).WithMany(p => p.AssetHistoryEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__AssetHist__Emplo__79FD19BE");

                entity.HasOne(d => d.ScrapApprovedByNavigation).WithMany(p => p.AssetHistoryScrapApprovedByNavigations)
                    .HasForeignKey(d => d.ScrapApprovedBy)
                    .HasConstraintName("FK__AssetHist__Scrap__7BE56230");
            });

            modelBuilder.Entity<AssetStatus>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AssetSta__3214EC0722A59445");

                entity.ToTable("AssetStatus", "AxionPro");

                entity.HasIndex(e => e.StatusName, "UQ__AssetSta__05E7698A7AA7A28E").IsUnique();

                // Common Entities
                entity.ConfigureBaseEntity();
                entity.Property(e => e.Description).HasMaxLength(255);               
                entity.Property(e => e.StatusName).HasMaxLength(50);
      
            });

            modelBuilder.Entity<AssetType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AssetTyp__3214EC077375A9AA");

                entity.ToTable("AssetType", "AxionPro");

                entity.HasIndex(e => e.TypeName, "UQ__AssetTyp__D4E7DFA8692FD5DF").IsUnique();

               
                entity.Property(e => e.Description).HasMaxLength(255);
               
                entity.Property(e => e.TypeName).HasMaxLength(100);
                // Common Entities
                entity.ConfigureBaseEntity();
            });

            modelBuilder.Entity<AssignmentStatus>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Assignme__3214EC07BCFD76FA");

                entity.ToTable("AssignmentStatus", "AxionPro");

            
                entity.Property(e => e.Description).HasMaxLength(255);
               
                entity.Property(e => e.StatusName).HasMaxLength(50);
                // Common Entities
                entity.ConfigureBaseEntity();
            });


            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC079EEE2ABB");

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
                entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07AC1B1F0C");

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

            modelBuilder.Entity<AttendanceRequest>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07DA4D2CA6");

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

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC078AAEC326");

                entity.ToTable("Candidate", "AxionPro");

                entity.HasIndex(e => e.PhoneNumber, "UQ__Candidat__85FB4E384EF74606").IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Candidat__A9D10534B67C36A0").IsUnique();

                entity.HasIndex(e => e.Aadhaar, "UQ__Candidat__C4B3336970636589").IsUnique();

                entity.HasIndex(e => e.Pan, "UQ__Candidat__C577943D11665D42").IsUnique();

                entity.HasIndex(e => e.CandidateReferenceCode, "UQ__Candidat__CF22B81A936408F4").IsUnique();

                entity.Property(e => e.Aadhaar).HasMaxLength(12);
                entity.Property(e => e.ActionStatus).HasMaxLength(20);
                entity.Property(e => e.AppliedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.CandidateReferenceCode).HasMaxLength(20);
                entity.Property(e => e.CurrentCompany).HasMaxLength(200);
                entity.Property(e => e.CurrentLocation).HasMaxLength(200);
                entity.Property(e => e.Education).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.ExpectedSalary).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ExperienceYears).HasColumnType("decimal(4, 1)");
                entity.Property(e => e.FewWords).HasMaxLength(1000);
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

            modelBuilder.Entity<CandidateCategorySkill>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC07853F4240");

                entity.ToTable("CandidateCategorySkill", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateCategorySkills)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Candi__15DA3E5D");

                entity.HasOne(d => d.Category).WithMany(p => p.CandidateCategorySkills)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Categ__16CE6296");
            });

            modelBuilder.Entity<CandidateHistory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC07F93DEB9C");

                entity.ToTable("CandidateHistory", "AxionPro");

                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.ReapplyAllowedAfter).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateHistories)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidate__Candi__17C286CF");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070D4D0C80");

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

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_TenderClient");

                entity.ToTable("Client", "AxionPro");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ClientName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientType).WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ClientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_ClientType");
            });

            modelBuilder.Entity<ClientType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ClientTy__3214EC078D984A16");

                entity.ToTable("ClientType", "AxionPro");

                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Country__3214EC070584DCC0");

                entity.ToTable("Country", "AxionPro");

                entity.Property(e => e.CountryCode).HasMaxLength(10);
                entity.Property(e => e.CountryName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC071E7B0B0B");

                entity.ToTable("Department", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.DepartmentName).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
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
                    .HasConstraintName("FK__Departmen__Depar__1A9EF37A");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Designat__3214EC07F9FF4C75");

                entity.ToTable("Designation", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.DesignationName).HasMaxLength(255);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tenant__3214EC0728DD7C6E");

                entity.ToTable("Tenant", "AxionPro");

                entity.HasIndex(e => e.TenantCode, "UQ_Tenant_TenantCode").IsUnique();

                entity.HasIndex(e => e.TenantEmail, "UQ__Tenant__F7C944DD7E3D53D9").IsUnique();

                entity.Property(e => e.CompanyEmailDomain).HasMaxLength(255);
                entity.Property(e => e.CompanyName).HasMaxLength(200);
                entity.Property(e => e.ContactNumber).HasMaxLength(20);
                entity.Property(e => e.ContactPersonName).HasMaxLength(100);
                entity.Property(e => e.TenantCode).HasMaxLength(100);
                entity.Property(e => e.TenantEmail).HasMaxLength(200);
            });

            modelBuilder.Entity<TenantEmailConfig>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenantEm__3214EC077A90B3A3");

                entity.ToTable("TenantEmailConfig", "AxionPro");

                entity.Property(e => e.FromEmail).HasMaxLength(200);
                entity.Property(e => e.FromName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.SmtpHost).HasMaxLength(200);
                entity.Property(e => e.SmtpPasswordEncrypted).HasMaxLength(500);
                entity.Property(e => e.SmtpUsername).HasMaxLength(200);

                entity.HasOne(d => d.Tenant).WithMany(p => p.TenantEmailConfigs)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_TenantEmailConfig_Tenant");
            });

            modelBuilder.Entity<TenantProfile>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenantPr__3214EC0796A1B93D");

                entity.ToTable("TenantProfile", "AxionPro");

                entity.Property(e => e.Address).HasMaxLength(300);
                entity.Property(e => e.BusinessType).HasMaxLength(100);
                entity.Property(e => e.Industry).HasMaxLength(100);
                entity.Property(e => e.LogoUrl).HasMaxLength(255);
                entity.Property(e => e.ThemeColor).HasMaxLength(50);
                entity.Property(e => e.WebsiteUrl).HasMaxLength(200);

                entity.HasOne(d => d.Tenant).WithMany(p => p.TenantProfiles)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_TenantProfile_Tenant");
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07E3264254");

                entity.ToTable("Employee", "AxionPro");

             
                entity.Property(e => e.EmployementCode).HasMaxLength(50);
                entity.Property(e => e.FirstName).HasMaxLength(100);                
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.MiddleName).HasMaxLength(100);
                entity.Property(e => e.OfficialEmail).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(200);
                // Common Entities
                entity.ConfigureBaseEntity();

                entity.HasOne(d => d.Designation).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Employee_Designation");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .HasConstraintName("FK_Employee_EmployeeType");

                entity.HasOne(d => d.Tenant).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_Employee_Tenant");
            });

            modelBuilder.Entity<EmployeeBankDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC072E9F930F");

                entity.ToTable("EmployeeBankDetail", "AxionPro");

                entity.Property(e => e.AccountNumber).HasMaxLength(50);
                entity.Property(e => e.AccountType).HasMaxLength(50);
                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.BankName).HasMaxLength(100);
                entity.Property(e => e.BranchName).HasMaxLength(100);
                entity.Property(e => e.Ifsccode)
                    .HasMaxLength(20)
                    .HasColumnName("IFSCCode");
                entity.Property(e => e.IsPrimaryAccount).HasDefaultValue(true);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Upiid)
                    .HasMaxLength(100)
                    .HasColumnName("UPIId");

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeBankDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeBank_Employee");
            });

            modelBuilder.Entity<EmployeeCategorySkill>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07BB3C49C9");

                entity.ToTable("EmployeeCategorySkill", "AxionPro");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasOne(d => d.Category).WithMany(p => p.EmployeeCategorySkills)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_EmployeeCategorySkill_Category");

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeCategorySkills)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeCategorySkill_Employee");
            });

            modelBuilder.Entity<EmployeeDailyAttendance>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC078C36F2DB");

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

            modelBuilder.Entity<EmployeeDependent>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC074B6A13E2");

                entity.ToTable("EmployeeDependents", "AxionPro");

                entity.Property(e => e.DependentName).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Relation).HasMaxLength(50);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDependents_Employee");
            });

            modelBuilder.Entity<EmployeeEducation>(entity =>
            {
                entity.ToTable("EmployeeEducation", "AxionPro");

                entity.Property(e => e.Degree).HasMaxLength(50);
                entity.Property(e => e.EducationGap).HasDefaultValue(false);
                entity.Property(e => e.GpaorPercentage)
                    .HasMaxLength(100)
                    .HasColumnName("GPAOrPercentage");
                entity.Property(e => e.GradeOrPercentage).HasMaxLength(50);
                entity.Property(e => e.InstituteName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.ReasonOfEducationGap).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(100);

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeEducations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEducation_Employee");
            });

            modelBuilder.Entity<EmployeeExperience>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC071B15DEB3");

                entity.ToTable("EmployeeExperience", "AxionPro");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsVerified).HasDefaultValueSql("(NULL)");
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ReasonForLeaving)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeExperiences)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeExperience_Employee");
            });

            modelBuilder.Entity<EmployeePersonalDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC074796302D");

                entity.ToTable("EmployeePersonalDetail", "AxionPro");

                entity.Property(e => e.AadhaarNumber).HasMaxLength(20);
                entity.Property(e => e.BloodGroup).HasMaxLength(10);
                entity.Property(e => e.DrivingLicenseNumber).HasMaxLength(20);
                entity.Property(e => e.EmergencyContactName).HasMaxLength(100);
                entity.Property(e => e.EmergencyContactNumber).HasMaxLength(15);
                entity.Property(e => e.MaritalStatus).HasMaxLength(20);
                entity.Property(e => e.Nationality).HasMaxLength(50);
                entity.Property(e => e.PanNumber).HasMaxLength(20);
                entity.Property(e => e.PassportNumber).HasMaxLength(20);
                entity.Property(e => e.VoterId).HasMaxLength(20);

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePersonalDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeePersonalDetail_Employee");
            });

            modelBuilder.Entity<EmployeePolicy>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0705F48FB6");

                entity.ToTable("EmployeePolicy", "AxionPro");

                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<EmployeeStatusHistory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0779153EAD");

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
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0760C5ED38");

                entity.ToTable("EmployeeType", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.TypeName).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeTypeBasicMenu>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07A773CB3F");

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

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Gender__3214EC07EF3CD03D");

                entity.ToTable("Gender", "AxionPro");

                entity.HasIndex(e => e.GenderName, "UQ__Gender__F7C177153AF55502").IsUnique();

                entity.Property(e => e.GenderName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InterviewFeedback>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC07C20637CB");

                entity.ToTable("InterviewFeedback", "AxionPro");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
                entity.Property(e => e.ReapplyAfter).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Candidate).WithMany(p => p.InterviewFeedbacks)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interview__Candi__2AD55B43");

                entity.HasOne(d => d.InterviewSchedule).WithMany(p => p.InterviewFeedbacks)
                    .HasForeignKey(d => d.InterviewScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interview__Inter__2BC97F7C");
            });

            modelBuilder.Entity<InterviewPanel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC07076FDC5F");

                entity.ToTable("InterviewPanel", "AxionPro");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.PanelName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InterviewPanelMember>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC07205CE980");

                entity.ToTable("InterviewPanelMember", "AxionPro");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Panel).WithMany(p => p.InterviewPanelMembers)
                    .HasForeignKey(d => d.PanelId)
                    .HasConstraintName("FK_InterviewPanelMember_Panel");

                //entity.HasOne(d => d.UserRole).WithMany(p => p.InterviewPanelMembers)
                //    .HasForeignKey(d => d.UserRoleId)
                //    .HasConstraintName("FK_InterviewPanelMember_UserRole");
            });

            modelBuilder.Entity<InterviewSchedule>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC073C91635C");

                entity.ToTable("InterviewSchedule", "AxionPro");

                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ScheduledDate).HasColumnType("datetime");

                entity.HasOne(d => d.Candidate).WithMany(p => p.InterviewSchedules)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK_InterviewSchedule_Candidate");

                entity.HasOne(d => d.Panel).WithMany(p => p.InterviewSchedules)
                    .HasForeignKey(d => d.PanelId)
                    .HasConstraintName("FK_InterviewSchedule_Panel");
            });

            modelBuilder.Entity<InterviewSdule>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC0702C95E26");

                entity.ToTable("InterviewSdule", "AxionPro");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.InterviewMode).HasMaxLength(50);
                entity.Property(e => e.ScheduledDateTime).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<LeaveAllocation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LeaveAll__3214EC070EA41DB5");

                entity.ToTable("LeaveAllocation", "AxionPro");

                entity.Property(e => e.AllocatedLeave)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(5, 2)");
                entity.Property(e => e.CanApply).HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.RemainingLeave)
                    .HasDefaultValue(0m)
                    .HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Employee).WithMany(p => p.LeaveAllocations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveAllo__Emplo__308E3499");

                entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveAllocations)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveAllo__Leave__318258D2");
            });

            modelBuilder.Entity<LeaveCarryForwardRule>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LeaveCar__3214EC07F271C2D1");

                entity.ToTable("LeaveCarryForwardRule", "AxionPro");

                entity.Property(e => e.ExpiryAfterYears).HasDefaultValue(0);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsSoftDelete).HasDefaultValue(false);
                entity.Property(e => e.MaxCarryForwardLeave).HasDefaultValue(40);
                entity.Property(e => e.SoftDeleteDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveCarryForwardRules)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveCarr__Leave__32767D0B");
            });

            modelBuilder.Entity<LeavePolicyByDesignation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LeavePol__3214EC07E3BFA2B4");

                entity.ToTable("LeavePolicyByDesignation", "AxionPro");

                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsCarriedForward).HasDefaultValue(false);
                entity.Property(e => e.IsSandwichRuleApplied).HasDefaultValue(false);
                entity.Property(e => e.IsSoftDelete).HasDefaultValue(false);
                entity.Property(e => e.MaxConsecutiveLeave).HasDefaultValue(0);
                entity.Property(e => e.NoOfLeave).HasDefaultValue(0);
                entity.Property(e => e.SoftDeleteDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicableGender).WithMany(p => p.LeavePolicyByDesignations)
                    .HasForeignKey(d => d.ApplicableGenderId)
                    .HasConstraintName("FK_LeavePolicyByDesignation_Gender");

                entity.HasOne(d => d.Designation).WithMany(p => p.LeavePolicyByDesignations)
                    .HasForeignKey(d => d.DesignationId)
                    .HasConstraintName("FK__LeavePoli__Desig__336AA144");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.LeavePolicyByDesignations)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeavePoli__Emplo__345EC57D");

                entity.HasOne(d => d.LeaveType).WithMany(p => p.LeavePolicyByDesignations)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeavePoli__Leave__3552E9B6");

                entity.HasOne(d => d.PolicyType).WithMany(p => p.LeavePolicyByDesignations)
                    .HasForeignKey(d => d.PolicyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeavePoli__Polic__36470DEF");
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LeaveTyp__3214EC0788A5EF9C");

                entity.ToTable("LeaveType", "AxionPro");

                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.LeaveName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<LoginCredential>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__LoginCre__3214EC0750429DFA");

                entity.ToTable("LoginCredential", "AxionPro");

                entity.HasIndex(e => e.LoginId, "UQ_LoginId").IsUnique();

                entity.Property(e => e.AddedById).HasDefaultValue(0L);
                
                entity.Property(e => e.HasFirstLogin).HasDefaultValue(true);
                entity.Property(e => e.IpAddressLocal).HasMaxLength(50);
                entity.Property(e => e.IpAddressPublic).HasMaxLength(50);
                
                entity.Property(e => e.Latitude).HasDefaultValue(0.0);
                entity.Property(e => e.LoginDevice).HasDefaultValue(0);
                entity.Property(e => e.LoginId).HasMaxLength(255);
                entity.Property(e => e.Longitude).HasDefaultValue(0.0);
                entity.Property(e => e.MacAddress).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(255);
                // Common Entities
                entity.ConfigureBaseEntity();

            });


            modelBuilder.Entity<MealAllowancePolicyByDesignation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__MealAllo__3214EC07BB4E4E52");

                entity.ToTable("MealAllowancePolicyByDesignation", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.BreakfastAllowance)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.DinnerAllowance)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.FixedFoodAllowance)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsMetro).HasDefaultValue(false);
                entity.Property(e => e.IsSoftDelete).HasDefaultValue(false);
                entity.Property(e => e.LunchAllowance)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.MetroBonus)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.MinDaysRequired).HasDefaultValue(0);
                entity.Property(e => e.RequiredDocuments).HasColumnType("text");
                entity.Property(e => e.SoftDeleteDateTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Designation).WithMany(p => p.MealAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealAllow__Desig__39237A9A");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.MealAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealAllow__Emplo__3A179ED3");

                entity.HasOne(d => d.PolicyType).WithMany(p => p.MealAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.PolicyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealAllow__Polic__3B0BC30C");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC079C437610");

                entity.ToTable("Operation", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.OperationName).HasMaxLength(200);
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<PolicyType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PolicyTy__3214EC07AAE08A64");

                entity.ToTable("PolicyType", "AxionPro");

                entity.HasIndex(e => e.PolicyName, "UQ__PolicyTy__2518511598309DFC").IsUnique();

                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsSoftDelete).HasDefaultValue(false);
                entity.Property(e => e.PolicyName).HasMaxLength(255);
                entity.Property(e => e.SoftDeleteDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjectChildModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectC__3214EC07F95C1A7A");

                entity.ToTable("ProjectChildModuleDetail", "AxionPro");

                entity.Property(e => e.ChildModuleName).HasMaxLength(255);
                entity.Property(e => e.ChildModuleUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ChildModuleURL");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.SubModule).WithMany(p => p.ProjectChildModuleDetails)
                    .HasForeignKey(d => d.SubModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubModule");
            });

            modelBuilder.Entity<ProjectModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectM__3214EC07C6DCCAB0");

                entity.ToTable("ProjectModuleDetail", "AxionPro");

                entity.Property(e => e.ModuleName).HasMaxLength(100);
                entity.Property(e => e.ModuleUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ModuleURL");
                entity.Property(e => e.Remark).HasMaxLength(255);
            });

            modelBuilder.Entity<ProjectSubModuleDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProjectS__3214EC07795E98E6");

                entity.ToTable("ProjectSubModuleDetail", "AxionPro");

                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsSubModuleDisplayInUi)
                    .HasDefaultValue(true)
                    .HasColumnName("IsSubModuleDisplayInUI");
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.SubModuleName).HasMaxLength(255);
                entity.Property(e => e.SubModuleUrl)
                    .HasMaxLength(255)
                    .HasColumnName("SubModuleURL");

                entity.HasOne(d => d.Module).WithMany(p => p.ProjectSubModuleDetails)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectSubModule_Module");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC0731D02168");

                entity.ToTable("RefreshToken", "AxionPro");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.CreatedByIp).HasMaxLength(50);
                entity.Property(e => e.ExpiryDate)
                    .HasDefaultValueSql("(dateadd(day,(5),getdate()))")
                    .HasColumnType("datetime");
                entity.Property(e => e.IsRevoked).HasDefaultValue(false);
                entity.Property(e => e.LoginId).HasMaxLength(255);
                entity.Property(e => e.ReplacedByToken).HasMaxLength(500);
                entity.Property(e => e.RevokedAt).HasColumnType("datetime");
                entity.Property(e => e.RevokedByIp).HasMaxLength(50);
                entity.Property(e => e.Token).HasMaxLength(500);

                entity.HasOne(d => d.Login).WithMany(p => p.RefreshTokens)
                    .HasPrincipalKey(p => p.LoginId)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__RefreshTo__Login__7132C993");
            });

            modelBuilder.Entity<RefreshToken1>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC077E8ED949");

                entity.ToTable("RefreshToken");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.CreatedByIp).HasMaxLength(50);
                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
                entity.Property(e => e.IsRevoked).HasDefaultValue(false);
                entity.Property(e => e.LoginId).HasMaxLength(255);
                entity.Property(e => e.ReplacedByToken).HasMaxLength(500);
                entity.Property(e => e.RevokedAt).HasColumnType("datetime");
                entity.Property(e => e.RevokedByIp).HasMaxLength(50);
                entity.Property(e => e.Token).HasMaxLength(500);

                entity.HasOne(d => d.Login).WithMany(p => p.RefreshToken1s)
                    .HasPrincipalKey(p => p.LoginId)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__RefreshTo__Login__60FC61CA");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0723371271");

                entity.ToTable("Role", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.DeletedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Remark).HasMaxLength(200);
                entity.Property(e => e.RoleName).HasMaxLength(100);
                entity.Property(e => e.UpdatedDateTime)
                    .HasDefaultValueSql("((0))")
                    .HasColumnType("datetime");
                // Common Entities
                entity.ConfigureBaseEntity();
                entity.HasOne(d => d.Tenant).WithMany(p => p.Roles)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Tenant");
            });

            modelBuilder.Entity<RoleModuleAndPermission>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_RoleModuleAndPermission_Id");

                entity.ToTable("RoleModuleAndPermission", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.ImageIcon)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ProjectChildModuleDetail).WithMany(p => p.RoleModuleAndPermissions)
                    .HasForeignKey(d => d.ProjectChildModuleDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RoleModuleAndPermission_ChildModule");
            });

            modelBuilder.Entity<ServiceProvider>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ServiceP__3214EC07B0695346");

                entity.ToTable("ServiceProvider", "AxionPro");

                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.Ceoname)
                    .HasMaxLength(255)
                    .HasColumnName("CEOName");
                entity.Property(e => e.CompanyEmail).HasMaxLength(255);
                entity.Property(e => e.CompanyName).HasMaxLength(255);
                entity.Property(e => e.CompanyType).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Fax).HasMaxLength(50);
                entity.Property(e => e.Gstnumber)
                    .HasMaxLength(50)
                    .HasColumnName("GSTNumber");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Location).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.PinCode).HasMaxLength(10);
                entity.Property(e => e.Profile).HasMaxLength(500);
                entity.Property(e => e.Remark).HasMaxLength(500);
                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(255)
                    .HasColumnName("WebsiteURL");
            });

            modelBuilder.Entity<ServiceProviderContact>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ServiceP__3214EC07BCDC9A65");

                entity.ToTable("ServiceProviderContact", "AxionPro");

                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.OfficialMobile).HasMaxLength(50);
                entity.Property(e => e.PersonalMobile).HasMaxLength(50);
                entity.Property(e => e.Position).HasMaxLength(255);

                entity.HasOne(d => d.ServiceProvider).WithMany(p => p.ServiceProviderContacts)
                    .HasForeignKey(d => d.ServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicePr__Servi__3DE82FB7");
            });

            modelBuilder.Entity<TenantEmailConfig>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenantEm__3214EC077A90B3A3");

                entity.ToTable("TenantEmailConfig", "AxionPro");

                entity.Property(e => e.FromEmail).HasMaxLength(200);
                entity.Property(e => e.FromName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.SmtpHost).HasMaxLength(200);
                entity.Property(e => e.SmtpPasswordEncrypted).HasMaxLength(500);
                entity.Property(e => e.SmtpUsername).HasMaxLength(200);

                entity.HasOne(d => d.Tenant).WithMany(p => p.TenantEmailConfigs)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK_TenantEmailConfig_Tenant");
            });

            modelBuilder.Entity<Tender>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Tender__3214EC076E847502");

                entity.ToTable("Tender", "AxionPro");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.TenderName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.TenderValue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Client).WithMany(p => p.Tenders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tender_ClientId");

                entity.HasOne(d => d.TenderStatus).WithMany(p => p.Tenders)
                    .HasForeignKey(d => d.TenderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tender__TenderSt__3EDC53F0");
            });

            modelBuilder.Entity<TenderProject>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenderPr__3214EC073A6AC46E");

                entity.ToTable("TenderProject", "AxionPro");

                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.EstimatedBudget).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.ProjectName).HasMaxLength(255);
                entity.Property(e => e.Remark).HasMaxLength(1000);
                entity.Property(e => e.StartDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Status).WithMany(p => p.TenderProjects)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TenderPro__Statu__40C49C62");

                entity.HasOne(d => d.TenderServiceProvider).WithMany(p => p.TenderProjects)
                    .HasForeignKey(d => d.TenderServiceProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenderProject_TenderServiceProvider");

            //    entity.HasOne(d => d.UserRole).WithMany(p => p.TenderProjects)
            //        .HasForeignKey(d => d.UserRoleId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__TenderPro__UserR__42ACE4D4");
            //
            });

            modelBuilder.Entity<TenderService>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenderSe__3214EC07620086F4");

                entity.ToTable("TenderService", "AxionPro");

                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.Tender).WithMany(p => p.TenderServices)
                    .HasForeignKey(d => d.TenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TenderSer__Tende__43A1090D");

                entity.HasOne(d => d.TenderServiceType).WithMany(p => p.TenderServices)
                    .HasForeignKey(d => d.TenderServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TenderSer__Tende__44952D46");
            });

            modelBuilder.Entity<TenderServiceHistory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenderSe__3214EC07F4765A9B");

                entity.ToTable("TenderServiceHistory", "AxionPro");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TenderServiceProvider>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenderSe__3214EC0765F55078");

                entity.ToTable("TenderServiceProvider", "AxionPro");

                entity.Property(e => e.ContractAmount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsInHouse).HasDefaultValue(false);
                entity.Property(e => e.IsPrimaryProvider).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.HasOne(d => d.Status).WithMany(p => p.TenderServiceProviders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TenderSer__Statu__4589517F");

                entity.HasOne(d => d.TenderServiceSpecification).WithMany(p => p.TenderServiceProviders)
                    .HasForeignKey(d => d.TenderServiceSpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenderServiceProvider_TenderServiceSpecification");

                entity.HasOne(d => d.TenderServiceSpecificationNavigation).WithMany(p => p.TenderServiceProviders)
                    .HasForeignKey(d => d.TenderServiceSpecificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TenderSer__Tende__467D75B8");
            });

            modelBuilder.Entity<TenderServiceSpecification>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenderSe__3214EC07FBF10311");

                entity.ToTable("TenderServiceSpecification", "AxionPro");

                entity.Property(e => e.EstimatedBudget).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.ProductPlatform).HasMaxLength(255);
                entity.Property(e => e.ProductSpecification).HasMaxLength(1000);
                entity.Property(e => e.SpecificationName).HasMaxLength(255);
                entity.Property(e => e.SpecificationType).HasMaxLength(50);

                entity.HasOne(d => d.TenderService).WithMany(p => p.TenderServiceSpecifications)
                    .HasForeignKey(d => d.TenderServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenderServiceSpecification_TenderService");
            });

            modelBuilder.Entity<TenderServiceType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TenderSe__3214EC072E505507");

                entity.ToTable("TenderServiceType", "AxionPro");

                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.ServiceName).HasMaxLength(255);
            });

            modelBuilder.Entity<TenderStatus>(entity =>
            {
                entity.ToTable("TenderStatus", "AxionPro");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.StatusName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TravelAllowancePolicyByDesignation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TravelAl__3214EC072DD6C86A");

                entity.ToTable("TravelAllowancePolicyByDesignation", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.AdvanceAllowed).HasDefaultValue(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsMetro).HasDefaultValue(false);
                entity.Property(e => e.IsSoftDelete).HasDefaultValue(false);
                entity.Property(e => e.MaxAdvanceAmount)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.MetroBonus)
                    .HasDefaultValue(0.00m)
                    .HasColumnType("decimal(10, 2)");
                entity.Property(e => e.ReimbursementPerKm)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("ReimbursementPerKM");
                entity.Property(e => e.RequiredDocuments).HasColumnType("text");
                entity.Property(e => e.SoftDeleteDateTime).HasColumnType("datetime");
                entity.Property(e => e.TravelClass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Designation).WithMany(p => p.TravelAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TravelAll__Desig__4959E263");

                entity.HasOne(d => d.EmployeeType).WithMany(p => p.TravelAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TravelAll__Emplo__4A4E069C");

                entity.HasOne(d => d.PolicyType).WithMany(p => p.TravelAllowancePolicyByDesignations)
                    .HasForeignKey(d => d.PolicyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TravelAll__Polic__4B422AD5");
            });

            modelBuilder.Entity<TravelMode>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__TravelMo__3214EC075A97AC69");

                entity.ToTable("TravelMode", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.TravelModeName).HasMaxLength(255);
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserAttendanceSetting>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserAtte__3214EC0750CE7646");

                entity.ToTable("UserAttendanceSetting", "AxionPro");

                entity.Property(e => e.AddedDateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.GeofenceLatitude).HasColumnType("decimal(10, 8)");
                entity.Property(e => e.GeofenceLongitude).HasColumnType("decimal(10, 8)");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.IsAllowed).HasDefaultValue(true);
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.ReportingTime).HasColumnType("datetime");
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
                entity.HasKey(e => e.Id).HasName("PK_UserRole");

                entity.ToTable("UserRole", "AxionPro");

                entity.Property(e => e.AddedDateTime).HasColumnType("datetime");
                entity.Property(e => e.AssignedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Remark).HasMaxLength(255);
                entity.Property(e => e.RemovedDateTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(ur => ur.Employee)
                      .WithMany(e => e.UserRoles)
                      .HasForeignKey(ur => ur.EmployeeId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_UserRole_Employee");

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_UserRole_Role");
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

          //  OnModelCreatingPartial(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
