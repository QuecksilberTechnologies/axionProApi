using ems.domain.Entity.Masters.RoleInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.CommonAndRoleBaseMenuConfig
{
   
        public class RoleConfiguration : IEntityTypeConfiguration<Role>
        {
            public void Configure(EntityTypeBuilder<Role> builder)
            {
                // Table name mapping (optional if it matches the default name)
                builder.ToTable("Role", "emp");

                // Primary Key
                builder.HasKey(r => r.Id);

                // Properties configuration
                builder.Property(r => r.RoleName)
                    .IsRequired()  // RoleName ko required bana rahe hain
                    .HasMaxLength(100); // Maximum length specify kar rahe hain

                builder.Property(r => r.Remark)
                    .HasMaxLength(500);  // Remark ko optional banaya gaya hai with maximum length

                builder.Property(r => r.IsActive)
                    .IsRequired()  // IsActive ko required set kiya gaya hai
                    .HasDefaultValue(true); // Default value set ki gayi hai

                builder.Property(r => r.AddedById)
                    .IsRequired(false);  // Optional field for AddedById

                builder.Property(r => r.AddedDateTime)
                    .IsRequired()  // AddedDateTime ko required banaya gaya hai
                    .HasDefaultValueSql("GETDATE()"); // Default value for current date and time

                builder.Property(r => r.UpdatedById)
                    .IsRequired(false);  // Optional field for UpdatedById

                builder.Property(r => r.UpdatedDateTime)
                    .IsRequired(false);  // Optional field for UpdatedDateTime
            /*
                // Relationships
                builder.HasMany(r => r.RolesPermissions)
                    .WithOne(rp => rp.Role)
                    .HasForeignKey(rp => rp.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);  // Role delete pe related RolesPermissions bhi delete ho jayenge

                builder.HasMany(r => r.UserRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);  // Role delete pe related UserRoles delete nahi honge

                // Indexes
                builder.HasIndex(r => r.RoleName) // Index for RoleName
                    .IsUnique();  // Ensure RoleName is unique in the database
            */
            }
        }
    }

 