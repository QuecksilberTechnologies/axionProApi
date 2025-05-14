using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
 public   class DesignationRepository : IDesignationRepository
    {
        private WorkforceDbContext _context;
        private ILogger<DesignationRepository> logger;

        public DesignationRepository(WorkforceDbContext context, ILogger<DesignationRepository> logger)
        {
            this._context = context;
            this.logger = logger;
        }

        public async Task<List<Designation>> CreateDesignationAsync(Designation designation)
        {
            try
            {

                designation.AddedDateTime = DateTime.Now; // or DateTime.UtcNow                
                await _context.Designations.AddAsync(designation);
                // Changes ko save karenge
                await _context.SaveChangesAsync();

                // Added role ko return karenge
                // `GetAllRolesAsync()` se returned IEnumerable ko List mein convert karenge
                return (await GetAllDesignationAsync())
                 .OrderByDescending(r => r.Id) // Latest Role पहले आएगा
                 .ToList();
            }
            catch (Exception ex)
            {
                // Exception ko log karenge
                logger.LogError(ex, "Error occurred while creating Designation.");
                throw;  // Rethrow the exception for further handling
            }
        }

        public Task<bool> DeleteDesignationAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Designation>> GetAllDesignationAsync()
        {

            try
            {
                logger.LogInformation("Fetching all designation from the database...");
                var designations = await _context.Designations.ToListAsync();

                if (!designations.Any())
                {
                    logger.LogWarning("No designation found in the database.");
                }

                return designations;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching designation.");
                return new List<Designation>();
            }
        }

        public Task<Designation> GetDesignationByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Designation>> UpdateDesignationAsync(Designation designation)
        {
            try
            {
                var existingDesignation = await _context.Designations.FirstOrDefaultAsync(l => l.Id == designation.Id);
                if (existingDesignation == null)
                {
                    logger.LogWarning("Designation type with ID {Designation} not found.", designation.Id);
                    return await GetAllDesignationAsync();
                }

                existingDesignation.DesignationName = designation.DesignationName;
                existingDesignation.Description = designation.Description;
                existingDesignation.IsActive = designation.IsActive;

                await _context.SaveChangesAsync();
                logger.LogInformation("designation  with ID {designationID} updated successfully.", designation.Id);
                return await GetAllDesignationAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating  with ID {designationID}.", designation.Id);
                throw;
            }
        }
    }
}
