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
    public class OperationRepository : IOperationRepository
    {
        private readonly WorkforceDbContext _context;
        private readonly ILogger<OperationRepository> _logger;

        public OperationRepository(WorkforceDbContext context, ILogger<OperationRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        

        public async Task<List<Operation>> CreateOperationAsync(Operation operation)
        {
            try
            {
                if (operation == null) throw new ArgumentNullException(nameof(operation));

                await _context.Operations.AddAsync(operation);
                await _context.SaveChangesAsync();

                _logger.LogInformation("operation type added successfully: {operation}", operation.OperationName);
                return await GetAllOperationAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating operation type.");
                throw;
            }
        }

        public Task<bool> DeleteOperationAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Operation>> GetAllOperationAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all Operations types from the database...");
                var operation = await _context.Operations.ToListAsync();

                if (!operation.Any())
                {
                    _logger.LogWarning("No Operations types found in the database.");
                }

                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Operations types.");
                return new List<Operation>();
            }
        }

        public async Task<Operation> GetOperationByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Operation>> UpdateOperationAsync(Operation operation)
        {
            try
            {
                var existingOperation = await _context.Operations.FirstOrDefaultAsync(l => l.Id == operation.Id);
                if (existingOperation == null)
                {
                    _logger.LogWarning("operation type with ID {operation} not found.", operation.Id);
                    return await GetAllOperationAsync();
                }

                existingOperation.OperationName = operation.OperationName;
                existingOperation.Remark = operation.Remark;
                existingOperation.IsActive = operation.IsActive;

                await _context.SaveChangesAsync();
                _logger.LogInformation("operation type with ID {operation} updated successfully.", operation.Id);
                return await GetAllOperationAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating operation type with ID {operation}.", operation.Id);
                throw;
            }

        }
    }
}
