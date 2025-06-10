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
    public class AssetRepository : IAssetRepository
    {
        private WorkforceDbContext _context;
        private ILogger<AssetRepository> _logger;

        public AssetRepository(WorkforceDbContext context, ILogger<AssetRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public Task<List<Asset>> AddAssetAsync(Asset asset)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AssetStatus>> AddAssetStatusByTenantAsync(AssetStatus assetStatus)
        {
            try
            {
                assetStatus.AddedDateTime = DateTime.Now;

                await _context.AssetStatuses.AddAsync(assetStatus);
                await _context.SaveChangesAsync();

                // ✅ Fetch all asset types for the same tenant
                var assetTypes = await GetAllAssetTypeByTenantIdAsync(assetStatus.TenantId);
                _logger.LogInformation("Fetched {Count} asset types for TenantId {TenantId} after adding AssetStatus.",
                    assetTypes.Count, assetStatus.TenantId);

                // ✅ Return all asset status records (latest first)
                return await _context.AssetStatuses
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating asset status.");
                throw;
            }
        }

        public Task<bool> DeleteAssetAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClientTypeAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Asset>> GetAllAssetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AssetStatus>> GetAllAssetStatusByTenantAsync(AssetStatus assetStatus)
        {
            try
            {
                if (assetStatus.TenantId <= 0)
                {
                    _logger.LogWarning("TenantId is missing or invalid while fetching Asset Status.");
                    return new List<AssetStatus>(); // OR throw exception if you want to enforce it harder
                }

                _logger.LogInformation("Dynamically fetching Asset Status records for TenantId: {TenantId}", assetStatus.TenantId);

                IQueryable<AssetStatus> query = _context.AssetStatuses
                    .Where(x => x.TenantId == assetStatus.TenantId); // ✅ mandatory applied here

                if (!string.IsNullOrWhiteSpace(assetStatus.StatusName))
                    query = query.Where(x => x.StatusName.Contains(assetStatus.StatusName));

                if (assetStatus.IsActive != null)
                    query = query.Where(x => x.IsActive == assetStatus.IsActive);

                if (assetStatus.IsSoftDeleted != null)
                    query = query.Where(x => x.IsSoftDeleted == assetStatus.IsSoftDeleted);

                if (assetStatus.AddedById != null)
                    query = query.Where(x => x.AddedById == assetStatus.AddedById);

                if (assetStatus.UpdatedById != null)
                    query = query.Where(x => x.UpdatedById == assetStatus.UpdatedById);

                if (assetStatus.DeletedById != null)
                    query = query.Where(x => x.DeletedById == assetStatus.DeletedById);

                var result = await query.OrderByDescending(x => x.AddedDateTime).ToListAsync();

                _logger.LogInformation("Fetched {Count} Asset Status records for TenantId: {TenantId}", result.Count, assetStatus.TenantId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while dynamically fetching Asset Status records for TenantId: {TenantId}", assetStatus.TenantId);
                return new List<AssetStatus>();
            }
        }


        public Task<List<AssetType>> GetAllAssetTypeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AssetType>> GetAllAssetTypeByTenantIdAsync(long byTenanatId)
        {
            try
            {
                _logger.LogInformation("Fetching Asset Types for TenantId: {TenantId} from the database...", byTenanatId);

                var assetTypes = await _context.AssetTypes
                    .Where(x => x.TenantId == byTenanatId)
                    .ToListAsync();

                if (assetTypes == null || !assetTypes.Any())
                {
                    _logger.LogWarning("No Asset Types found in the database for TenantId: {TenantId}.", byTenanatId);
                    return new List<AssetType>();
                }

                _logger.LogInformation("Successfully retrieved {Count} Asset Types for TenantId: {TenantId}.", assetTypes.Count, byTenanatId);
                return assetTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Asset Types for TenantId: {TenantId}.", byTenanatId);
                return new List<AssetType>();
            }
        }

        public async Task<Asset> GetAssetByIdFromTenantAsync(int id)
        {
            try
            {
                var asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);

                if (asset == null)
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found.", id);
                }

                return asset;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching asset with ID {AssetId}.", id);
                throw; // Exception को rethrow कर दें ताकि calling code को पता चल सके
            }
        }

        public async Task<List<AssetStatus>> GetAssetsStatus()
        {
            try
            {
                _logger.LogInformation("Fetching all Asset Types from the database...");

                var assetsStatus = await _context.AssetStatuses.ToListAsync();

                if (assetsStatus == null || !assetsStatus.Any())
                {
                    _logger.LogWarning("No Asset Status found in the database.");
                    return new List<AssetStatus>();
                }

                _logger.LogInformation("Successfully retrieved {Count} Asset Status.", assetsStatus.Count);
                return assetsStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Asset status.");
                return new List<AssetStatus>();
            }
        }

        public async Task<bool> IsAssetDuplicate(Asset asset)
        {
            try
            {
                return await _context.Assets.AnyAsync(a =>
                    (a.SerialNumber == asset.SerialNumber || a.Barcode == asset.Barcode) &&
                    a.Id != asset.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking for duplicate asset with ID {AssetId}.", asset.Id);
                throw;  // Exception को रिथ्रो कर दें ताकि calling code को पता चल सके
            }
        }

        public async Task<AssetStatus> UpdateAssetStatusByTenantAsync(AssetStatus assetStatus)
        {
            try
            {
                // Existing record ko DB se fetch karo (Id + TenantId for safety)
                var existingStatus = await _context.AssetStatuses
                    .FirstOrDefaultAsync(x => x.Id == assetStatus.Id && x.TenantId == assetStatus.TenantId);

                if (existingStatus == null)
                {
                    _logger.LogWarning("AssetStatus with Id {Id} not found for TenantId {TenantId}.", assetStatus.Id, assetStatus.TenantId);
                    return null!; // Or throw custom NotFoundException
                }

                // Update fields
                existingStatus.StatusName = assetStatus.StatusName;
                existingStatus.Description = assetStatus.Description;
                existingStatus.IsActive = assetStatus.IsActive;
                existingStatus.UpdatedById = assetStatus.UpdatedById;
                existingStatus.UpdatedDateTime = DateTime.Now;

                // Save to DB
                await _context.SaveChangesAsync();

                return existingStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating AssetStatus.");
                throw;
            }
        }


        async Task<List<Asset>> IAssetRepository.UpdateAssetAsync(Asset asset)
        {
            try
            {
                // Database से existing asset ढूंढें
                var existingAsset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == asset.Id);

                if (existingAsset == null)
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found.", asset.Id);
                    return null; // अगर Asset नहीं मिला तो null रिटर्न करें
                }

                // Existing Asset के फ़ील्ड्स को अपडेट करें
                existingAsset.IsRepairable = asset.IsRepairable;
                existingAsset.WarrantyExpiryDate = asset.WarrantyExpiryDate;
                existingAsset.AssetStatusId = asset.AssetStatusId;
                existingAsset.IsAssigned = asset.IsAssigned;
                existingAsset.IsActive = asset.IsActive;
                existingAsset.UpdatedById = 1; // ट्रैकिंग के लिए UpdatedBy
               // existingAsset.UpdatedDate = DateTime.UtcNow;

                // बदलावों को सेव करें
                await _context.SaveChangesAsync();

                _logger.LogInformation("Asset with ID {AssetId} updated successfully.", asset.Id);

                // यदि आपकी repository की डिफ़िनिशन List<Asset> रिटर्न करने की मांग करती है, तो आप updated asset के साथ एक list return कर सकते हैं।
                return new List<Asset> { existingAsset };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating asset with ID {AssetId}.", asset.Id);
                throw;  // Rethrow exception for further handling
            }
        }


    }
}
