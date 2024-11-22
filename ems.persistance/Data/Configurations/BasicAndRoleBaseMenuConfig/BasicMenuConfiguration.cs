using ems.domain.Entity.RoleModulePermission;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.domain.Entity.BasicMenuInfo;

namespace ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig
{
    public class BasicMenuConfiguration : IEntityTypeConfiguration<BasicMenu>
    {
        public void Configure(EntityTypeBuilder<BasicMenu> builder)
        {
            // Set the table name and schema
            builder.HasKey(e => e.Id).HasName("PK_BasicMenu_Id");

            builder.ToTable("BasicMenu", "emp");

            builder.Property(e => e.AddedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.MenuName).HasMaxLength(100);
            builder.Property(e => e.MenuUrl)
                .HasMaxLength(255)
                .HasColumnName("MenuURL");
            builder.Property(e => e.Remark).HasMaxLength(200);
            builder.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

            builder.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("FK_BasicMenu_ParentMenu");



        }
    }
}
