using ems.application.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context
{
    public class EmsDbContext : DbContext,IEmsDbContext
    {
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options)
        {

        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
