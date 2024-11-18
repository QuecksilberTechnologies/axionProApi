using ems.domain.Entity.Masters.ProjectModuleInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig
{
    public class ProjectModuleDetailConfiguration : IEntityTypeConfiguration<ProjectModuleDetail>
    {
        public void Configure(EntityTypeBuilder<ProjectModuleDetail> builder)
        {
            builder.ToTable("ProjectModuleDetail", "emp");
        }
    }
}
