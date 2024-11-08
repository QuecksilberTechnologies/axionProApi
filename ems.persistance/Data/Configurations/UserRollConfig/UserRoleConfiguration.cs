using ems.domain.Entity.UserRoleModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ems.persistance.Data.Configurations.UserRollConfig
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Table name and schema
            builder.ToTable("UserRole", "emp");

            // Primary key
            builder.HasKey(ur => ur.Id);

            // Property configurations
            builder.Property(ur => ur.Id)
                .IsRequired();

            builder.Property(ur => ur.EmployeeId)
                .IsRequired();

            builder.Property(ur => ur.RoleId)
                .IsRequired();

            builder.Property(ur => ur.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(ur => ur.Remark)
                .HasMaxLength(255);

            builder.Property(ur => ur.AssignedDateTime)
                .IsRequired(false);

            builder.Property(ur => ur.RemovedDateTime)
                .IsRequired(false);

            builder.Property(ur => ur.AssignedById)
                .IsRequired();

            builder.Property(ur => ur.RoleStartDate)
                .IsRequired(false);

            builder.Property(ur => ur.AddedById)
                .IsRequired();

            builder.Property(ur => ur.AddedDateTime)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ur => ur.UpdatedById)
                .IsRequired(false);

            builder.Property(ur => ur.UpdatedDateTime)
                .IsRequired(false);

            // Foreign key relationships
            
            builder.HasOne(ur => ur.Employee)
                .WithMany(e => e.UserRoles) // Assuming `Employee` has a collection of `UserRoles`
                .HasForeignKey(ur => ur.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            /*
            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles) // Assuming `Role` has a collection of `UserRoles`
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Indexes for performance optimization if needed
            builder.HasIndex(ur => new { ur.EmployeeId, ur.RoleId })
                .IsUnique(); // Prevents duplicate role assignments for the same employee
            */
        }
    }
}
