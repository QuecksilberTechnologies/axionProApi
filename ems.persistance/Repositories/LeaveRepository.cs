using ems.application.Interfaces.IRepositories;
using ems.domain.Entity;
using ems.persistance.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ems.persistance.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<LeaveRepository> _logger;

        public LeaveRepository(WorkforceDbContext context, ILogger<LeaveRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<LeaveType>> GetAllLeaveAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all leave types from the database...");
                var leaveTypes = await _context.LeaveTypes.ToListAsync();

                if (!leaveTypes.Any())
                {
                    _logger.LogWarning("No leave types found in the database.");
                }

                return leaveTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching leave types.");
                return new List<LeaveType>();
            }
        }

        public async Task<List<LeaveType>> CreateLeaveAsync(LeaveType leaveType)
        {
            try
            {
                if (leaveType == null) throw new ArgumentNullException(nameof(leaveType));

                await _context.LeaveTypes.AddAsync(leaveType);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Leave type added successfully: {LeaveName}", leaveType.LeaveName);
                return await GetAllLeaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating leave type.");
                throw;
            }
        }

        public async Task<bool> DeleteLeaveAsync(int leaveId)
        {
            try
            {
                var leave = await _context.LeaveTypes.FindAsync(leaveId);
                if (leave == null)
                {
                    _logger.LogWarning("Leave type with ID {LeaveId} not found.", leaveId);
                    return false;
                }

                _context.LeaveTypes.Remove(leave);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Leave type with ID {LeaveId} deleted successfully.", leaveId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting leave type with ID {LeaveId}.", leaveId);
                return false;
            }
        }

        public async Task<LeaveType?> GetLeaveByIdAsync(int leaveId)
        {
            return await _context.LeaveTypes.FindAsync(leaveId);
        }

        public async Task<List<LeaveType>> UpdateLeaveAsync(LeaveType leaveType)
        {
            try
            {
                var existingLeave = await _context.LeaveTypes.FirstOrDefaultAsync(l => l.Id == leaveType.Id);
                if (existingLeave == null)
                {
                    _logger.LogWarning("Leave type with ID {LeaveId} not found.", leaveType.Id);
                    return await GetAllLeaveAsync();
                }

                existingLeave.LeaveName = leaveType.LeaveName;
                existingLeave.Description = leaveType.Description;
                existingLeave.IsActive = leaveType.IsActive;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Leave type with ID {LeaveId} updated successfully.", leaveType.Id);
                return await GetAllLeaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating leave type with ID {LeaveId}.", leaveType.Id);
                throw;
            }
        }
    }
}
