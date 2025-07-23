using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<CountryRepository> _logger;
        public CountryRepository(WorkforceDbContext context, ILogger<CountryRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Country>> GetAllAsync() => await _context.Countries.ToListAsync();
        public async Task<Country> GetByIdAsync(int id) => await _context.Countries.FindAsync(id);
        public async Task AddAsync(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
        }

        
    }

}
