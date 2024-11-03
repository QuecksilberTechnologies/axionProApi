using ems.domain.Entity.UserCredential;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ems.persistance.Data.Configurations.LoginDetailConfig
{
    public class LoginDetailConfiguration
    {
        public LoginDetailConfiguration(EntityTypeBuilder<LoginCredential> entity)
        {
            // entity.Property(e => e.Id).HasColumnName("ID");
            // Other mappings...
        }
    }
}
