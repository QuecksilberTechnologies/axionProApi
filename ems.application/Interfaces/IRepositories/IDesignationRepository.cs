using ems.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.application.Interfaces.IRepositories
{
    public interface IDesignationRepository
    {
        // Create a new Designation
        Task<List<Designation>> CreateDesignationAsync(Designation designation);

        // Get a designation by its Id
        Task<Designation> GetDesignationByIdAsync(int Id);

        Task<bool> CheckDuplicateValueAsync(long? TenantId, string value);

        // Get all designation
        Task<List<Designation>> GetAllDesignationAsync(long? TenantId,bool isActive);
        Task<List<Designation>> GetAllActiveDesignationAsync(long? TenantId);
        Task<bool> AutoCreateDesignationAsync(Designation designation);
        // Update an existing designation
        Task<bool> UpdateDesignationAsync(Designation designation);

        // Delete a designation by its Id
        Task<bool> DeleteDesignationAsync(Designation designation);
    }
}
