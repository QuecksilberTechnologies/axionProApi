using ems.domain.Entity.Masters.ProjectModuleInfo;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Data.Configurations.BasicAndRoleBaseMenuConfig
{
    public class ProjectSubModuleDetailConfiguration : IEntityTypeConfiguration<ProjectSubModuleDetail>
    {
        

        public void Configure(EntityTypeBuilder<ProjectSubModuleDetail> builder)
        {  
            // Table name mapping (optional if it matches the default name)
            builder.ToTable("ProjectSubModuleDetail", "emp");

            // Primary Key
            builder.HasKey(r => r.Id);

 
 


        }
    }
}
