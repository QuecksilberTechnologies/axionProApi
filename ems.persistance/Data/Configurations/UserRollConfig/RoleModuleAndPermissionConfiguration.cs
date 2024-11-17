using ems.domain.Entity.RoleModulePermission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.UserRollConfig
{
    public class RoleModuleAndPermissionConfiguration : IEntityTypeConfiguration<RoleModuleAndPermission>
    {

      

        public void Configure(EntityTypeBuilder<RoleModuleAndPermission> builder)
        {
            builder.ToTable("RoleModuleAndPermission", "emp");
        }
    }
}