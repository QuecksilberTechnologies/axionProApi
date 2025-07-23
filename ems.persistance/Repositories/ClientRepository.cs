﻿using ems.application.Interfaces.IRepositories;
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
    public class ClientRepository : IClientRepository
    {
        private ILogger _logger;
        private WorkforceDbContext _context;
        private ILogger<ClientRepository> logger;


        public ClientRepository(WorkforceDbContext context, ILogger<ClientRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<List<ClientType>> CreateClientTypeAsync(ClientType  clientType)
        {
            try
            {
                if (clientType == null) throw new ArgumentNullException(nameof(clientType));

                await _context.ClientTypes.AddAsync(clientType);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Leave type added successfully: {clientType}", clientType.TypeName);
                return await GetAllClientTypeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating leave type.");
                throw;
            }
        }

        public Task<bool> DeleteClientTypeAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientType>> GetAllClientTypeAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all client types from the database...");
                var clientTypes = await _context.ClientTypes.ToListAsync();

                if (!clientTypes.Any())
                {
                    _logger.LogWarning("No client types found in the database.");
                }

                return clientTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching client types.");
                return new List<ClientType>();
            }
        }

        public Task<ClientType> GetClientTypeByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientType>> UpdateClientTypeAsync(ClientType clientType)
        {
            try
            {
                var existingLeave = await _context.ClientTypes.FirstOrDefaultAsync(l => l.Id == clientType.Id);
                if (existingLeave == null)
                {
                    _logger.LogWarning("Leave type with ID {LeaveId} not found.", clientType.Id);
                     throw new NotImplementedException();
                }

                existingLeave.TypeName = clientType.TypeName;
                existingLeave.Description = clientType.Description;
                existingLeave.Remark = clientType.Remark;
                existingLeave.IsActive = clientType.IsActive;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Leave type with ID {LeaveId} updated successfully.", clientType.Id);
                return await GetAllClientTypeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating leave type with ID {LeaveId}.", clientType.Id);
                throw;
            }
        }
    }
}
