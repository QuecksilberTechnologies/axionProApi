using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface ILeaveRepository
    {
        // Create a new role
        Task<List<LeaveType>> CreateLeaveAsync(LeaveType leaveType);

        // Get a role by its Id
        Task<LeaveType> GetLeaveByIdAsync(int leaveId);

        // Get all roles
        Task<List<LeaveType>> GetAllLeaveAsync();

        // Update an existing role
        Task<List<LeaveType>> UpdateLeaveAsync(LeaveType leaveType);

        // Delete a role by its Id
        Task<bool> DeleteLeaveAsync(int leaveId);
    }
}
 
