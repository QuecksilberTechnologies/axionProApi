using ems.domain.Entity.BasicMenuInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.CommonAndRoleBaseMenuConfig
{

    public class CommonMenuConfig : IEntityTypeConfiguration<BasicMenu>
    {
        /*
        public void Configure(EntityTypeBuilder<CommonMenu> builder)
        {

            // Define table name and schema if necessary
            builder.ToTable("CommonMenu", "emp");

            // Configure primary key
            builder.HasKey(menu => menu.Id);
            // Configure foreign key relationship with EmployeeType
            builder.HasOne(menu => menu.EmployeeType)
                   .WithMany(employeeType => employeeType.BasicMenu) // No error should occur here now
                   .HasForeignKey(menu => menu.EmployeeTypeId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_EmployeeType");

            // Configure properties with constraints
            builder.Property(menu => menu.MenuName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(menu => menu.MenuUrl)
                   .HasMaxLength(500);

            builder.Property(menu => menu.Remark)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(menu => menu.ForPlatform)
                   .IsRequired(false); // Nullable

            builder.Property(menu => menu.ImageIcon)
                     .IsRequired(false); // Nullable

            builder.Property(menu => menu.IsMenuDisplayInUi)
                       .IsRequired();

            builder.Property(menu => menu.IsSubMenu)
                   .IsRequired();

            builder.Property(menu => menu.IsDisplayable)
                   .IsRequired();

            builder.Property(menu => menu.IsActive)
                   .IsRequired();

            builder.Property(menu => menu.HasAccess)
                   .IsRequired();

            // Define the self-referencing relationship for parent-child menus
            builder.HasOne(menu => menu.ParentMenu)
                   .WithMany(menu => menu.InverseParentMenu)
                   .HasForeignKey(menu => menu.ParentMenuId)
                   .OnDelete(DeleteBehavior.Restrict); // Use appropriate delete behavior

            // Optional: Configure default values if necessary
            builder.Property(menu => menu.IsMenuDisplayInUi)
                   .HasDefaultValue(true);
            builder.Property(menu => menu.IsActive)
                   .HasDefaultValue(true);
            builder.Property(menu => menu.IsDisplayable)
                   .HasDefaultValue(true);
        }
    
    
    }




        }*/

        public void Configure(EntityTypeBuilder<BasicMenu> builder)
        {
            // Define table name and schema
            builder.ToTable("CommonMenu", "emp");

            // Primary Key
            builder.HasKey(cm => cm.Id);

            // Properties Configuration
            builder.Property(cm => cm.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(cm => cm.MenuName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(cm => cm.MenuUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(cm => cm.Remark)
                .HasMaxLength(500)
                .IsRequired();

            
           

            builder.HasOne(cm => cm.ParentMenu)
                .WithMany(pm => pm.InverseParentMenu)
                .HasForeignKey(cm => cm.ParentMenuId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}