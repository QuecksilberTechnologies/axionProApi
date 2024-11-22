using ems.domain.Entity.RoleModulePermission;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig
{
    public class UserRolesPermissionOnModuleConfiguration : IEntityTypeConfiguration<RoleModuleAndPermission>
    {
        public void Configure(EntityTypeBuilder<RoleModuleAndPermission> builder)
        {
            // Set the table name and schema
         builder.Property(e => e.AddedDateTime)
    .HasColumnType("datetime")
    .IsRequired();

builder.Property(e => e.UpdatedDateTime)
    .HasColumnType("datetime");

builder.Property(e => e.Remark)
    .HasMaxLength(255);

            // Module Relationship
            builder.HasOne(d => d.SubModule).WithMany()
                    .HasForeignKey(d => d.SubModuleId)
                    .OnDelete(DeleteBehavior.Cascade);
 

// Operation Relationship
builder.HasOne(d => d.Operations)
    .WithMany(p => p.RoleModuleAndPermissions) // Corrected navigation property
    .HasForeignKey(d => d.OperationId)
    .OnDelete(DeleteBehavior.ClientSetNull)
    .HasConstraintName("FK_RoleModuleAndPermission_Operation");

// Role Relationship
builder.HasOne(d => d.Role)
    .WithMany(p => p.RolesPermR) // Corrected navigation property
    .HasForeignKey(d => d.RoleId)
    .OnDelete(DeleteBehavior.ClientSetNull)
    .HasConstraintName("FK_RoleModuleAndPermission_Role");




        


        }
    }
    }
