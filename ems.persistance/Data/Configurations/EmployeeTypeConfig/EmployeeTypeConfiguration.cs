using ems.domain.Entity.UserCredential;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ems.persistance.Data.Configurations.EmployeeTypeConfig
{
    public class EmployeeTypeConfiguration : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> builder)
        {
            // Table Name
            builder.ToTable("EmployeeType", "emp");

            // Primary Key
            builder.HasKey(et => et.Id)
                   .HasName("PK__Employee__3214EC0701E8E042");

            // Property Configurations
            builder.Property(et => et.TypeName)
                   .HasMaxLength(255)
                   .IsRequired(false); // Set nullable as per your requirement

            builder.Property(et => et.Description)
                   .HasMaxLength(255);

            builder.Property(et => et.Remark)
                   .HasMaxLength(255);
            builder.Property(et => et.RoleId)
                 .IsRequired(true);


            builder.Property(et => et.IsActive)
                   .HasDefaultValue(true); // Default value for IsActive

            builder.Property(et => et.AddedDateTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()"); // Default date on creation

            builder.Property(et => et.UpdatedDateTime)
                   .HasColumnType("datetime");

            /*
            // One-to-Many Relationship with Employees
            builder.HasMany(et => et.Employees)
                   .WithOne(e => e.EmployeeType)
                   .HasForeignKey(e => e.EmployeeTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many Relationship with CommonMenus
            builder.HasMany(et => et.CommonMenus)
                   .WithOne(cm => cm.EmployeeType)
                   .HasForeignKey(cm => cm.EmployeeTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            One-to-One Relationship with Role
            builder.HasOne(et => et.Role)
                   .WithMany(r => r.Id)
                   .HasForeignKey(et => et.RoleId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_EmployeeType_Role");
            */
        }
    }
}
