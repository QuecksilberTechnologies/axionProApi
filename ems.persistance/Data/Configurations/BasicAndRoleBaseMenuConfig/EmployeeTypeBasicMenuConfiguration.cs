using ems.domain.Entity.RoleModulePermission;
using ems.domain.Entity.UserCredential;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig
{
    public class EmployeeTypeBasicMenuConfiguration : IEntityTypeConfiguration<EmployeeTypeBasicMenu>
    {
        public void Configure(EntityTypeBuilder<EmployeeTypeBasicMenu> builder)
        {
            // Set the table name and schema
            builder.ToTable("EmployeeTypeBasicMenu", "emp");

            // Set the primary key
            builder.HasKey(e => e.Id).HasName("PK__Employee__3214EC07335FD665");

            // Configure properties
            builder.Property(e => e.AddedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.IsMenuDisplayInUi)
                .HasColumnName("IsMenuDisplayInUI");

            builder.Property(e => e.UpdatedDateTime)
                .HasColumnType("datetime");

            // Configure relationships
            builder.HasOne(d => d.BasicMenu)
                .WithMany(p => p.EmployeeTypeBasicMenus)
                .HasForeignKey(d => d.BasicMenuId)
                .HasConstraintName("FK_EmployeeTypeBasicMenu_BasicMenu");

          
        }
    }

}

