using ems.domain.Entity.BasicMenuInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig
{

    public class BasicMenuConfiguration : IEntityTypeConfiguration<BasicMenu>
    {
         

        public void Configure(EntityTypeBuilder<BasicMenu> builder)
        {
            // Define table name and schema
            builder.ToTable("BasicMenu", "emp");

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