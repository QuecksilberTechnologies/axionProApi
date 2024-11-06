using ems.domain.Entity.UserCredential;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ems.persistance.Data.Configurations.LoginDetailConfig
{
    public class LoginCredentialConfiguration : IEntityTypeConfiguration<LoginCredential>
    {
        public void Configure(EntityTypeBuilder<LoginCredential> builder)
        {
            // Table Name Configuration
            builder.ToTable("LoginCredential", "emp"); // Specify schema as "emp"

            // Primary Key Configuration (if applicable)
          //  builder.HasKey(lc => lc.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.EmployeeId);
            // LoginId - Required and Maximum Length
            builder.Property(x => x.LoginId)
                   .IsRequired()
                   .HasMaxLength(100);

            // Password - Required and Maximum Length
            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(200); // Adjust the length for storing hashed password

            // HasFirstLogin - Required
            builder.Property(x => x.HasFirstLogin)
                   .IsRequired();

            // MacAddress - Optional, with Max Length
            builder.Property(x => x.MacAddress)
                   .HasMaxLength(100)
                   .IsRequired(false); // Optional field

            // IpAddress - Optional, with Max Length
            builder.Property(x => x.IpAddress)
                   .HasMaxLength(100)
                   .IsRequired(false); // Optional field

            // Latitude and Longitude - Optional
            builder.Property(x => x.Latitude)
                   .IsRequired();

            builder.Property(x => x.Longitude)
                   .IsRequired();

            // LoginDevice - Required with default value
            builder.Property(x => x.LoginDevice)
                   .IsRequired()
                   .HasDefaultValue(1); // Default to 1 (Mobile/Device Type)

            // AddedById - Required
            builder.Property(x => x.AddedById)
                   .IsRequired();

            // AddedDateTime - Default value as current timestamp
            builder.Property(x => x.AddedDateTime)
                   .HasDefaultValueSql("GETDATE()"); // Default value SQL function

            // UpdatedById and UpdatedDateTime - Optional
            builder.Property(x => x.UpdatedById)
                   .IsRequired(false);

            builder.Property(x => x.UpdatedDateTime)
                   .IsRequired(false);

            // IsActive - Required with default value
            builder.Property(x => x.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true); // Default to active

            // Remark - Optional with Max Length
            builder.Property(x => x.Remark)
                   .HasMaxLength(500)
                   .IsRequired(false);
        }
    }
}
