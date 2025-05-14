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

        // Get all designation
        Task<List<Designation>> GetAllDesignationAsync();

        // Update an existing designation
        Task<List<Designation>> UpdateDesignationAsync(Designation designation);

        // Delete a designation by its Id
        Task<bool> DeleteDesignationAsync(int Id);
    }
}
