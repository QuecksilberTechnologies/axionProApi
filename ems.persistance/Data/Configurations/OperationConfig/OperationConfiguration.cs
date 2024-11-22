using ems.domain.Entity.BasicMenuInfo;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ems.domain.Entity.Masters.ModuleOperation;
using Azure;
using Operation = ems.domain.Entity.Masters.ModuleOperation.Operation;

namespace ems.persistance.Data.Configurations.OperationConfig
{
    public class OperationConfiguration: IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            // Set the table name and schema
       
            builder.HasKey(e => e.Id).HasName("PK__Operatio__3214EC079906B6BF");

            builder.ToTable("Operation", "emp");

            builder.Property(e => e.AddedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.OperationName).HasMaxLength(200);
            builder.Property(e => e.Remark).HasMaxLength(200);
            builder.Property(e => e.UpdateDateTime).HasColumnType("datetime");



        }
    }
}
 
