using ems.application.Interfaces.IContext;
using Microsoft.EntityFrameworkCore;

namespace ems.persistance.Data.Context
{
    public class EmsDbContext : DbContext, IEmsDbContext
    {
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Assembly ke sabhi configuration classes ko automatically apply karna
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
